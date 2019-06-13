﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using NSSDB;
using SharedDB;
using NSSAgent;
using NSSServices.Filters;
using WIM.Security.Authentication.Basic;
using Microsoft.AspNetCore.Mvc;
using SharedAgent;
using WIM.Services.Analytics;
using WIM.Utilities.ServiceAgent;
using WIM.Services.Resources;
using WIM.Services.Messaging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WIM.Services.Middleware;
using WIM.Security.Authentication;
using WIM.Security.Authorization;
using WIM.Resources;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WIM.Services.Security.Authentication.JWTBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NSSServices
{
    public class Startup
    {
        private string _hostKey = "USGSWiM_HostName";
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            if (env.IsDevelopment()) {
                builder.AddUserSecrets<Startup>();
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            Configuration = builder.Build();
        }//end startup       

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Transient objects are always different; a new instance is provided to every controller and every service.
            //Singleton objects are the same for every object and every request.
            //Scoped objects are the same within a request, but different across different requests.

            //Configure injectable obj
            services.AddScoped<IAnalyticsAgent, GoogleAnalyticsAgent>((gaa) => new GoogleAnalyticsAgent(Configuration["AnalyticsKey"]));
            services.Configure<APIConfigSettings>(Configuration.GetSection("APIConfigSettings"));
            services.Configure<JwtBearerSettings>(Configuration.GetSection("JwtBearerSettings"));
            
            //provides access to httpcontext
            services.AddHttpContextAccessor();
            services.AddScoped<NSSServiceAgent>();
            services.AddScoped<INSSAgent>(x => x.GetRequiredService<NSSServiceAgent>());
            services.AddScoped<IAuthenticationAgent>(x => x.GetRequiredService<NSSServiceAgent>());
            services.AddScoped<ISharedAgent, SharedAgent.SharedAgent>();

           // services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            



            // Add framework services.
            services.AddDbContext<NSSDBContext>(options =>
                                                        options.UseNpgsql(String.Format(Configuration
                                                            .GetConnectionString("Connection"), Configuration["dbuser"], Configuration["dbpassword"], Configuration["dbHost"]),
                                                            //default is 1000, if > maxbatch, then EF will group requests in maxbatch size
                                                            opt => { opt.MaxBatchSize(1000);opt.UseNetTopologySuite(); })
                                                            //.EnableSensitiveDataLogging()
                                                            );
            services.AddDbContext<SharedDBContext>(options =>
                                                        options.UseNpgsql(String.Format(Configuration
                                                            .GetConnectionString("Connection"), Configuration["dbuser"], Configuration["dbpassword"], Configuration["dbHost"]),
                                                            //default is 1000, if > maxbatch, then EF will group requests in maxbatch size
                                                            opt => opt.MaxBatchSize(1000))
                                                            //.EnableSensitiveDataLogging()
                                                            );

            services.AddScoped<IAnalyticsAgent, GoogleAnalyticsAgent>((gaa) => new GoogleAnalyticsAgent(Configuration["AnalyticsKey"]));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })//.AddBasicAuthentication()
            .AddJwtBearer(options =>
            {
                //options.Events = new JWTBearerAuthenticationEvents();
                options.Events = new JWTBearerAuthenticationEvents();
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtBearerSettings:SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
            services.AddAuthorization(options => loadAutorizationPolicies(options));

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                                                                 .AllowAnyMethod()
                                                                 .AllowAnyHeader()
                                                                 .WithExposedHeaders(new string[]{this._hostKey,X_MessagesDefault.msgheader})
                                                                 .AllowCredentials());
            });
            
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                //for hypermedia
                options.Filters.Add(new NSSHypermedia());

                //needed for geojson deserializer
                options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(Polygon)));
                options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(MultiPolygon)));
                options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(Geometry)));
            })
                    //.AddXmlSerializerFormatters()
                    //.AddXmlDataContractSeria‌​lizerFormatters()
                    .AddJsonOptions(options => loadJsonOptions(options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // global policy - assign here or on each controller
            app.UseX_Messages(option => { option.HostKey = this._hostKey;});
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.Use_Analytics();
            app.UseMvc();
        }

        #region Helper Methods
        private void loadAutorizationPolicies(AuthorizationOptions options)
        {
            //https://www.thereformedprogrammer.net/a-better-way-to-handle-authorization-in-asp-net-core/
            //https://jasonwatmore.com/post/2019/01/08/aspnet-core-22-role-based-authorization-tutorial-with-example-api

            options.AddPolicy(
                Policy.Managed,
                policy => policy.RequireRole(Role.Admin, Role.Manager));
            options.AddPolicy(
                Policy.AdminOnly,
                policy => policy.RequireRole(Role.Admin));
        }
        private void loadJsonOptions(MvcJsonOptions options)
        {
            //options.SerializerSettings.TraceWriter = new memoryTraceWriter();
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
            options.SerializerSettings.TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;
            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
            //needed for geojson serializer
            foreach (var converter in GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel(), 4326)).Converters)
            { options.SerializerSettings.Converters.Add(converter); }
        }
  
        #endregion


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using NSSDB;
using NSSAgent;
using WiM.Security.Authentication.Basic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using WiM.Services.Middleware;
using WiM.Services.Analytics;
using WiM.Utilities.ServiceAgent;
using WiM.Services.Resources;

namespace NSSServices
{
    public class Startup
    {
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
            //Configure injectable obj
            services.Configure<APIConfigSettings>(Configuration.GetSection("APIConfigSettings"));
            services.AddScoped<INSSAgent, NSSServiceAgent>();
            services.AddScoped<IBasicUserAgent, NSSServiceAgent>();

            // Add framework services.
            services.AddDbContext<NSSDBContext>(options =>
                                                        options.UseNpgsql(String.Format(Configuration
                                                            .GetConnectionString("NSSConnection"), Configuration["dbuser"], Configuration["dbpassword"], Configuration["dbHost"]),
                                                            //default is 1000, if > maxbatch, then EF will group requests in maxbatch size
                                                            opt => opt.MaxBatchSize(1000))
                                                            .EnableSensitiveDataLogging());

            services.AddScoped<IAnalyticsAgent, GoogleAnalyticsAgent>((gaa) => new GoogleAnalyticsAgent(Configuration["AnalyticsKey"]));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = BasicDefaults.AuthenticationScheme;
            }).AddBasicAuthentication();
            services.AddAuthorization(options => loadAutorizationPolicies(options));

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                                                                 .AllowAnyMethod()
                                                                 .AllowAnyHeader()
                                                                 .AllowCredentials());
            });

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            })
                    .AddXmlSerializerFormatters()
                    .AddXmlDataContractSeria‌​lizerFormatters()
                    .AddJsonOptions(options => loadJsonOptions(options));
        }

     

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // global policy - assign here or on each controller
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }

        #region Helper Methods
        private void loadAutorizationPolicies(AuthorizationOptions options)
        {   
            options.AddPolicy(
                "CanModify",
                policy => policy.RequireRole("Administrator", "Manager"));
            options.AddPolicy(
                "Restricted",
                policy => policy.RequireRole("Administrator", "Manager", "General"));
            options.AddPolicy(
                "AdminOnly",
                policy => policy.RequireRole("Administrator"));
        }
        private void loadJsonOptions(MvcJsonOptions options)
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
            options.SerializerSettings.TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;
            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
        }
  
        #endregion


    }
}

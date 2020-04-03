using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace NSSServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(serverOptions => serverOptions.AddServerHeader = false)
                //.ConfigureLogging((context,logging)=> {
                //var env = context.HostingEnvironment;
                //var config = context.Configuration.GetSection("Logging");
                //logging.AddConfiguration(config);
                //logging.AddConsole();
                //logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                //})
                .UseStartup<Startup>();
            })
            .Build();

            host.Run();
        }
    }

}

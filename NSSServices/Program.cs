using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace NSSServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => options.AddServerHeader = false)
                //.ConfigureLogging((context,logging)=> {
                    //var env = context.HostingEnvironment;
                    //var config = context.Configuration.GetSection("Logging");
                    //logging.AddConfiguration(config);
                    //logging.AddConsole();
                    //logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                //})
                .UseStartup<Startup>()
                .Build();
    }

}

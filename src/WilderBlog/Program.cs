using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WilderBlog.Data;

namespace WilderBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                  .ConfigureAppConfiguration(ConfigureConfiguration)
                  .UseStartup<Startup>()
                  .Build();

            if (args.Contains("/seed"))
            {
                Seed(host).Wait();
            }

            host.Run();
        }

        private static async Task Seed(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<WilderInitializer>();
                await initializer.SeedAsync();
            }
        }

        private static void ConfigureConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // Reset to remove the old configuration sources to give us complete control
            builder.Sources.Clear();

            builder.SetBasePath(ctx.HostingEnvironment.ContentRootPath)
              .AddJsonFile("config.Production.json", false, true)
              .AddEnvironmentVariables();
        }
    }
}

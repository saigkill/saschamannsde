using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using WilderBlog.Data;

namespace WilderBlog.Services
{
    public class WilderContextFactory : IDesignTimeDbContextFactory<WilderContext>
    {
        public WilderContext CreateDbContext(string[] args)
        {
            // Create a configuration 
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("config.Production.json")
              .Build();

            return new WilderContext(new DbContextOptionsBuilder<WilderContext>().Options, config);
        }
    }
}

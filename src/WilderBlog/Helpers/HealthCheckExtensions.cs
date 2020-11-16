using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WilderBlog.Data;

namespace WilderBlog.Helpers
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection coll, IConfiguration config)
        {
            var connectionString = config["WilderDb:ConnectionString"];
            var instrumentationKey = config["ApplicationInsights:InstrumentationKey"];

            coll.AddHealthChecks()
                .AddSqlServer(connectionString, name: "DbConnection")
                .AddSqlServer(connectionString,
                    "SELECT COUNT(*) FROM BlogStory",
                    "BlogDb")
                .AddDbContextCheck<WilderContext>()
                .AddApplicationInsightsPublisher(instrumentationKey: instrumentationKey); ;

            return coll;
        }
    }
}

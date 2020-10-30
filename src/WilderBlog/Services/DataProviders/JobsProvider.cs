using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace WilderBlog.Services.DataProviders
{
    public class JobsProvider : DataProvider<Job>
    {
        public JobsProvider(IHostEnvironment env) : base(env, "jobs.json")
        {

        }

        public override IEnumerable<Job> Get()
        {
            return base.Get().OrderByDescending(p => p.Id).ToList();
        }
    }

    public class Job
    {
        public int Id { get; set; }
        public string Jobtitle { get; set; }
        public string Company { get; set; }
        public string Years { get; set; }
        public string Tasks { get; set; }
        public string Stack { get; set; }
        public string Link { get; set; }
    }
}

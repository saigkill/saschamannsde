using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace WilderBlog.Services.DataProviders
{
    public class CertsDeProvider : DataProvider<CertDe>
    {
        public CertsDeProvider(IHostEnvironment env) : base(env, "certificatesDe.json")
        {
        }

        public override IEnumerable<CertDe> Get()
        {
            return base.Get().OrderByDescending(p => p.Id).ToList();
        }
    }

    public class CertDe
    {
        public int Id { get; set; }
        public string Certtitle { get; set; }
        public string Provider { get; set; }
        public string Years { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
    }
}
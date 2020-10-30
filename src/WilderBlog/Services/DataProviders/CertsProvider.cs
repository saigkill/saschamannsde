using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace WilderBlog.Services.DataProviders
{
    public class CertsProvider : DataProvider<Cert>
    {
        public CertsProvider(IHostEnvironment env) : base(env, "certificates.json")
        {

        }

        public override IEnumerable<Cert> Get()
        {
            return base.Get().OrderByDescending(p => p.Id).ToList();
        }
    }

    public class Cert
    {
        public int Id { get; set; }
        public string Certtitle { get; set; }
        public string Provider { get; set; }
        public string Years { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
    }
}

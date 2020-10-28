using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WilderBlog.Services.DataProviders
{
    public class TalksProvider : DataProvider<Talk>
    {
        public TalksProvider(IHostEnvironment env) : base(env, "talks.json")
        {

        }

        public override IEnumerable<Talk> Get()
        {
            return base.Get().OrderByDescending(p => p.Date).ToList();
        }
    }

    public enum TalkType
    {
        Unknown = 0,
        Slideshare
    }

    public class Talk
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Where { get; set; }
        public string Link { get; set; }
        public string Blurp { get; set; }
        public DateTime Date { get; set; }
        public TalkType TalkType { get; set; }
    }
}

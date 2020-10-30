using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace WilderBlog.Services.DataProviders
{
    public class TestimonialsProvider : DataProvider<Testimonial>
    {
        public TestimonialsProvider(IHostEnvironment env) : base(env, "testimonials.json")
        {

        }

        public override IEnumerable<Testimonial> Get()
        {
            return base.Get().OrderByDescending(p => p.Id).ToList();
        }
    }

    public class Testimonial
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Blurp { get; set; }
        public string Recommender { get; set; }
        public string RecommenderJob { get; set; }
        public string RecommenderLocation { get; set; }
        public string Relationship { get; set; }
        public string ImagePath { get; set; }
        public string Date { get; set; }
    }
}

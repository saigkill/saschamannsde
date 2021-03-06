using System;
namespace WilderBlog.Data
{
    public class Publications
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public DateTime DatePublished { get; set; }
        public bool IsBook { get; set; }
        public string Link { get; set; }
        public string PublicationName { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string Identifier { get; set; }
    }
}

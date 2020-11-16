using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Logging;
using WilderBlog.Models;

namespace WilderBlog.Services
{
    public class AdService
    {
        private readonly ILogger<AdService> _logger;

        public AdService(ILogger<AdService> logger)
        {
            _logger = logger;
        }

        public HtmlString InlineAdd()
        {
            var ranges = new List<AdDateRange>()
      {
        new AdDateRange( // Fallback
          DateTime.MinValue.ToString(),
          DateTime.MaxValue.ToString(),
          @"<div class=""card-text""><small>If you liked this article, so <a target=""_blank"" href=""https:www.buymeacoffee.com/PE0y8DF""><img src=""~/img/misc/buymeacoffee.jpg"" alt=""Buy me a coffee"" width=""12%""></a>:-).</small></div>"
        )
      };
            var now = DateTime.Now;
            var ads = ranges.Where(r => r.Start <= now && r.End >= now).FirstOrDefault();

            var item = new Random().Next(0, ads.Ads.Length);

            return new HtmlString(ads.Ads[item]);
        }

        public HtmlString SidebarAdd()
        {
            var ranges = new List<AdDateRange>()
      {
        new AdDateRange( // Fallback
          DateTime.MinValue.ToString(),
          DateTime.MaxValue.ToString(),
          @"<div class=""card-text""><small>If you liked this article, so <a target=""_blank"" href=""https:www.buymeacoffee.com/PE0y8DF""><img src=""~/img/misc/buymeacoffee.jpg"" alt=""Buy me a coffee"" width=""12%""></a>:-).</small></div>"
        )
      };
            var now = DateTime.Now;
            var ads = ranges.Where(r => r.Start <= now && r.End >= now).FirstOrDefault();

            var item = new Random().Next(0, ads.Ads.Length);

            return new HtmlString(ads.Ads[item]);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WilderBlog.Services.DataProviders;

namespace WilderBlog.Controllers
{
    [Route("[controller]")]
    public class TalksController : Controller
    {
        private TalksProvider _talks;

        public TalksController(TalksProvider talks)
        {
            _talks = talks;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View(_talks.Get());
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = System.DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
    }
}

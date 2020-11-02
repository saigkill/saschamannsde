using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WilderBlog.Data;

namespace WilderBlog.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private IWilderRepository _repo;

        public SearchController(IWilderRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.Term = "";
            return View(new BlogResult());
        }

        [HttpGet("{term}/{page:int?}")]
        public async Task<IActionResult> Pager(string term, int page = 1)
        {
            ViewBag.Term = term;

            var results = await _repo.GetStoriesByTerm(term, 10, page);
            return View("Index", results);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                 CookieRequestCultureProvider.DefaultCookieName,
                 CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                 new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                 );
            return LocalRedirect(returnUrl);
        }
    }
}

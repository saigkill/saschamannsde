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
    }
}

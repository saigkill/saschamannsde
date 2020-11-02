using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WilderBlog.Data;

namespace WilderBlog.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        [Route("changepwd")]
        public async Task<IActionResult> ChangePwd([FromServices] UserManager<WilderUser> userManager,
          string username,
          string oldPwd,
          string newPwd)
        {
            var user = await userManager.FindByEmailAsync(username);
            if (user == null) return BadRequest(new { success = false });
            var result = await userManager.ChangePasswordAsync(user, oldPwd, newPwd);
            if (result.Succeeded) return Ok(new { success = true });
            else return BadRequest(new { success = false, errors = result.Errors });
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

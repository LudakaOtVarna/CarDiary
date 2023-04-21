namespace CarDiary.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected string GetUserId() => this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult AccessDenied()
		{
			return this.View();
		}
	}
}

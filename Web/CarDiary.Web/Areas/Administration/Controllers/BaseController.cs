using CarDiary.Web.ViewModels;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CarDiary.Common;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CarDiary.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class BaseController : Controller
    {
        protected string GetUserId() => this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

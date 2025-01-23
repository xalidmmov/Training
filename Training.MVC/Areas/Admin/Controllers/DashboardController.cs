using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training.BL.Enum;

namespace Training.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = nameof(Roles.Admin))]
	public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

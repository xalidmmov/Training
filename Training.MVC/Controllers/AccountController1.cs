using Microsoft.AspNetCore.Mvc;

namespace Training.MVC.Controllers
{
    public class AccountController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

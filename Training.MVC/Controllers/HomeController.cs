using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Training.DAL.DAL;
using Training.MVC.Models;

namespace Training.MVC.Controllers
{
    public class HomeController(TrainingDbContext _context) : Controller
    {
        public async Task< IActionResult> Index()
        {
            var data=await _context.Trainers.Include(x=>x.Category).Where(x=>x.IsDeleted==false).ToListAsync();


            return View(data);
        }

       
    }
}

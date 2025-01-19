using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Training.BL.Service.Abstracts;
using Training.BL.ViewModels.Trainer;
using Training.DAL.DAL;
using Training.MVC.Extensions;

namespace Training.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainingController(ITrainerService _service,ICategoryService categoryservice,IWebHostEnvironment env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(categoryservice.GetAllAsync().Result, "Id", "CName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TrainerCreateVM vm)
        {
            if (vm.CoverImage != null)
            {
                if (!vm.CoverImage.IsValidSize(3000))
                {
                    ModelState.AddModelError("Image", "Image size must be less than 300 kb");
                }


            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(categoryservice.GetAllAsync().Result, "Id", "CName");
                return View();

            }
            vm.TrainerImg = await vm.CoverImage!.UploadAsync(env.WebRootPath, "trainers", "imgs");
            await _service.Create(vm);
            return RedirectToAction(nameof(Index));




        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            bool condition = await _service.Delete(id);

            if (!condition) return NotFound();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
			ViewBag.Categories = new SelectList(categoryservice.GetAllAsync().Result, "Id", "CName");
			ViewBag.Data = await _service.Get(id);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, TrainerCreateVM vm)
        {
			var data = await _service.Get(id);
			if (vm.CoverImage == null)
			{
				vm.TrainerImg = data.TrainerImg;
				await _service.UpdateAsync(id, vm);
				return RedirectToAction(nameof(Index));

			}
			if (!ModelState.IsValid)
			{
				ViewBag.Categories = new SelectList(categoryservice.GetAllAsync().Result, "Id", "CName");
				return View();

			}
			vm.TrainerImg = await vm.CoverImage!.UploadAsync(env.WebRootPath, "trainers", "imgs");
			await _service.UpdateAsync(id, vm);

			return RedirectToAction(nameof(Index));

		}
    }
}

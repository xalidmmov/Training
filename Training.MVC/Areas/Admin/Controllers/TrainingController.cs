using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using Training.BL.Enum;
using Training.BL.Service.Abstracts;
using Training.BL.ViewModels.Trainer;
using Training.DAL.DAL;
using Training.MVC.Extensions;

namespace Training.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = nameof(Roles.Admin))]
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
			if (vm.CoverImage == null ||
	  !vm.CoverImage.IsValidType("image") ||
	  !vm.CoverImage.IsValidSize(3000))
			{
				ModelState.AddModelError("Image", "Please upload a valid image file less than 300 KB.");
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
                if (!vm.CoverImage!.IsValidType("image"))
                {
                    vm.TrainerImg = data.TrainerImg;
                    await _service.UpdateAsync(id, vm);
                    return RedirectToAction(nameof(Index));
                }
			}
			
			vm.TrainerImg = await vm.CoverImage!.UploadAsync(env.WebRootPath, "trainers", "imgs");
			await _service.UpdateAsync(id, vm);

			return RedirectToAction(nameof(Index));

		}
    }
}

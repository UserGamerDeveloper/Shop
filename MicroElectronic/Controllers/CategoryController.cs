using MicroElectronic.Domain.ViewModels.Category;
using MicroElectronic.Domain.ViewModels.Pagination;
using MicroElectronic.Service.Implementations;
using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroElecWebStore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IEquipmentService _equipmentService;
        private readonly IBufferedFileUploadService _bufferedFileUploadService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, 
            IEquipmentService equipmentService, IBufferedFileUploadService bufferedFileUploadService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _equipmentService = equipmentService;
            _bufferedFileUploadService = bufferedFileUploadService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _categoryService.GetCategories();
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }

        public async Task<IActionResult> EditMode()
        {
            var response = await _categoryService.GetCategories();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _categoryService.GetCategory(id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView("Edit", response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IFormFile? file, [FromForm]CategoryViewModel model)
        {
            if (model.Name != null)
            {
                //if(file?.Length > 0)
                {
                    var imageUrl = await _bufferedFileUploadService.UploadFile(file);
                    model.ImageUrl = imageUrl;
                }   
                if (model.Id == 0)
                {
                    await _categoryService.Create(model);   
                }
                else
                {
                    await _categoryService.Update(model.Id, model);
                }
            }
            return RedirectToAction("EditMode", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.GetCategory(id);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView("Delete", response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return RedirectToAction("EditMode", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            //if(model.Id != 0)
            {
                //System.IO.File.Delete("D:/repos/MicroElectronic/MicroElectronic/wwwroot" + model.ImageUrl);
                await _categoryService.DeleteCategory(model.Id);
            }

            return RedirectToAction("EditMode", "Category");
        }
    }
}

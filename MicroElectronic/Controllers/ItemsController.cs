using MicroElectronic.Domain.Enum;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.ViewModels.Category;
using MicroElectronic.Domain.ViewModels.Equipment;
using MicroElectronic.Domain.ViewModels.Pagination;
using MicroElectronic.Service.Implementations;
using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroElecWebStore.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IEquipmentService _equipmentService;
        private readonly IBufferedFileUploadService _bufferedFileUploadService;

        public ItemsController(ILogger<ItemsController> logger, ICategoryService categoryService, 
            IEquipmentService equipmentService, IBufferedFileUploadService bufferedFileUploadService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _equipmentService = equipmentService;
            _bufferedFileUploadService = bufferedFileUploadService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            int category,
            int page = 1,
            int configurator = 0,
            int socket = -1,
            int memory = -1,
            int processorPin = -1,
            int videocardPin = -1,
            int power = 0)
        {
            var response = await _equipmentService.GetEquipments(category);
            IEnumerable<Equipment> items = response.Data.ToArray();
            switch (category)
            {
                case (int)CategoryId.Motherboard:
                    {
                        if (socket != -1)
                        {
                            items = items.Where(x => x.One == socket);
                        }
                        if (memory != -1)
                        {
                            items = items.Where(x => x.Two == memory);
                        }
                        if (processorPin != -1)
                        {
                            items = items.Where(x => x.Three <= processorPin);
                        }
                        break;
                    }
                case (int)CategoryId.Processor:
                    {
                        if (socket != -1)
                        {
                            items = items.Where(x => x.One == socket);
                        }
                        break;
                    }
                case (int)CategoryId.Memory:
                    {
                        if (memory != -1)
                        {
                            items = items.Where(x => x.One == memory);
                        }
                        break;
                    }
                case (int)CategoryId.Videocard:
                    {
                        if (videocardPin != -1)
                        {
                            items = items.Where(x => x.One <= videocardPin);
                        }
                        break;
                    }
                case (int)CategoryId.Powerblock:
                    {
                        if (videocardPin != -1)
                        {
                            items = items.Where(x => x.One >= videocardPin);
                        }
                        if (processorPin != -1)
                        {
                            items = items.Where(x => x.Two >= processorPin);
                        }
                        if (power != 0)
                        {
                            items = items.Where(x => x.Power >= power);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            var title = await _categoryService.GetCategory(category);
            int pageSize = 5;
            var count = items.Count();
            items = items.Skip((page - 1) * pageSize)
                .Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel indexViewModel = new IndexViewModel()
            {
                PageViewModel = pageViewModel,
                Equipments = items
            };
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewData["CategoryTitle"] = title.Data.Name;
                ViewData["CategoryId"] = category;
                ViewData["Configurator"] = configurator;
                ViewBag.processorPin = processorPin;
                ViewBag.videocardPin = videocardPin;
                ViewBag.memory = memory;
                ViewBag.socket = socket;
                ViewBag.power = power;
                return View(indexViewModel);
            }

            return RedirectToAction("Index", "Items");
        }

        public async Task<IActionResult> EditMode(int category, int page = 1)
        {
            var response = await _equipmentService.GetEquipments(category);
            var title = await _categoryService.GetCategory(category);
            int pageSize = 5;
            var count = response.Data.Count();
            var items = response.Data.Skip((page - 1) * pageSize)
                .Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel indexViewModel = new IndexViewModel()
            {
                PageViewModel = pageViewModel,
                Equipments = items
            };
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewData["CategoryTitle"] = title.Data.Name;
                ViewData["CategoryId"] = category;
                return View(indexViewModel);
            }

            return RedirectToAction("Index", "Items");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return PartialView(new EquipmentViewModel());

            var response = await _equipmentService.GetEquipment(id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView(response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IFormFile? file, [FromForm]EquipmentViewModel model)
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
                    await _equipmentService.Create(model);
                }
                else
                {
                    await _equipmentService.Update(model.Id, model);
                }
            }
            return RedirectToAction("EditMode", "Items", new { category = TempData["CategoryId"] });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _equipmentService.GetEquipment(id);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView(response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return RedirectToAction("EditMode", "Items", new { category = TempData["CategoryId"] });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EquipmentViewModel model)
        {
            //if(model.Id != 0)
            {
                //System.IO.File.Delete("D:/repos/MicroElectronic/MicroElectronic/wwwroot" + model.ImageUrl);
                await _equipmentService.Delete(model.Id);
            }

            return RedirectToAction("EditMode", "Items", new { category = TempData["CategoryId"] });
        }

    }
}

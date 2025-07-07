using MicroElectronic.Domain.Enum;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Configurator;
using MicroElectronic.Domain.ViewModels.Equipment;
using MicroElectronic.Service.Implementations;
using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroElectronic.Controllers
{
    public class ConfiguratorController : Controller
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IApplicationItemService _applicationItemService;

        public ConfiguratorController(IEquipmentService equipmentService, IApplicationItemService applicationItemService)
        {
            _equipmentService = equipmentService;
            _applicationItemService = applicationItemService;
        }

        //[Route("Catalog/Equipments/EquipmentPage")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ConfiguratorViewModel();
            var powerConf = 0;

            var id = TempData.Peek("Processor");
            if (id != null)
            {
                var response = await _equipmentService.GetEquipment((int)id);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Catalog");
                }
                model.Proccessor = response.Data;
                powerConf += model.Proccessor.Power;
            }

            id = TempData.Peek("Motherboard");
            if (id != null)
            {
                var response = await _equipmentService.GetEquipment((int)id);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Catalog");
                }
                model.Motherboard = response.Data;
            }

            id = TempData.Peek("Videocard");
            if (id != null)
            {
                var response = await _equipmentService.GetEquipment((int)id);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Catalog");
                }
                model.Videocard = response.Data;
                powerConf += model.Videocard.Power;
            }

            id = TempData.Peek("Memory");
            if (id != null)
            {
                var response = await _equipmentService.GetEquipment((int)id);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Catalog");
                }
                model.RAM = response.Data;
            }

            id = TempData.Peek("SSD");
            if (id != null)
            {
                var response = await _equipmentService.GetEquipment((int)id);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Catalog");
                }
                model.SSD = response.Data;
            }

            id = TempData.Peek("Powerblock");
            if (id != null)
            {
                var response = await _equipmentService.GetEquipment((int)id);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Catalog");
                }
                if (powerConf > response.Data.Power)
                {
                    TempData["Powerblock"] = null;
                }
                else
                {
                    model.Powerblock = response.Data;
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddToBasket(int userId, int[] equipId)
        {
            if (equipId != null)
            {
                foreach (var item in equipId)
                {
                    var response = await _applicationItemService.Add(item, userId);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index", "Catalog");
                    }
                }
                ResetTempData();
            }
            return RedirectToAction("Index", "Configurator");
        }

        [HttpGet]
        public async Task<IActionResult> Clear()
        {
            ResetTempData();
            return RedirectToAction("Index", "Configurator");
        }

        private void ResetTempData()
        {
            TempData["Motherboard"] = null;
            TempData["Processor"] = null;
            TempData["Memory"] = null;
            TempData["Videocard"] = null;
            TempData["SSD"] = null;
            TempData["Powerblock"] = null;
        }

        [HttpGet]
        public async Task<IActionResult> AddItem(int idItem, int idCategory)
        {
            switch (idCategory)
            {
                case (int)CategoryId.Motherboard:
                    {
                        TempData["Motherboard"] = idItem;
                        break;
                    }
                case (int)CategoryId.Processor:
                    {
                        TempData["Processor"] = idItem;
                        break;
                    }
                case (int)CategoryId.Memory:
                    {
                        TempData["Memory"] = idItem;
                        break;
                    }
                case (int)CategoryId.Videocard:
                    {
                        TempData["Videocard"] = idItem;
                        break;
                    }
                case (int)CategoryId.SSD:
                    {
                        TempData["SSD"] = idItem;
                        break;
                    }
                case (int)CategoryId.Powerblock:
                    {
                        TempData["Powerblock"] = idItem;
                        break;
                    }
                default:
                    {
                        return RedirectToAction("Index", "Catalog");
                    }
            }

            return RedirectToAction("Index", "Configurator");
        }
    }
}

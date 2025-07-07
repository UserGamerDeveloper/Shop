using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroElectronic.Controllers
{
    public class ItemController : Controller
    {
        private readonly IEquipmentService _equipmentService;

        public ItemController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        //[Route("Catalog/Equipments/EquipmentPage")]
        public async Task<IActionResult> Index(int id, int configurator = 0)
        {
            var response = await _equipmentService.GetEquipment(id);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewData["Title"] = response.Data.Name;
                ViewData["Configurator"] = configurator;
                return View(response.Data);
            }

            return RedirectToAction("Index", "Catalog");
        }
    }
}

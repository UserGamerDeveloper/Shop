using AspNetCoreHero.ToastNotification.Abstractions;
using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroElectronic.Controllers
{
    public class OrderListController : Controller
    {
        private readonly IApplicationItemService _applicationItemService;
        public OrderListController(IApplicationItemService applicationItemService)
        {
            _applicationItemService = applicationItemService;
        }

        public async Task<IActionResult> OrderList()
        {
            var userId = Int32.Parse(User.FindFirst("Id").Value);

            var response = await _applicationItemService.GetApplicationItems(userId);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add(int equipId, int userId)
        {
            var response = await _applicationItemService.Add(equipId, userId);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView("AddInfoSuccess");
            }

            return PartialView("AddInfoSuccess");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var response = await _applicationItemService.Remove(itemId);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("MyApplicationItems", "ApplicationItems");
            }

            return RedirectToAction("MyApplicationItems", "ApplicationItems");    
        }

        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(int itemId, string operation)
        {
            var response = await _applicationItemService.ChangeQuantity(itemId, operation);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new EmptyResult();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ClearItems(int userId)
        {
            var response = await _applicationItemService.Clear(userId);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new EmptyResult();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

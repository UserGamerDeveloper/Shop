using MicroElectronic.Domain.ViewModels.Order;
using MicroElectronic.Service.Implementations;
using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MicroElectronic.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IApplicationItemService _applicationItemService;
        public OrderController(IOrderService orderService, IApplicationItemService applicationItemService)
        {
            _orderService = orderService;
            _applicationItemService = applicationItemService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = Int32.Parse(User.FindFirst("Id").Value);

            var response = await _orderService.GetItemsList(userId);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView(response.Data);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderViewModel model)
        {
            var userId = Int32.Parse(User.FindFirst("Id").Value);

            var newOrder = new OrderViewModel()
            {
                OrderId = model.OrderId,
                UserId = userId
            };
            var response = await _orderService.CreateOrder(newOrder);
            var responseClear = await _applicationItemService.Clear(userId);

            if (response.StatusCode == System.Net.HttpStatusCode.OK && responseClear.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("MyApplicationItems", "ApplicationItems");
            }
            return RedirectToAction("Index", "Home");
        }


    }
}

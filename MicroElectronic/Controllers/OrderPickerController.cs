using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Order;
using MicroElectronic.Domain.ViewModels.User;
using MicroElectronic.Service.Implementations;
using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroElectronic.Controllers
{
    public class OrderPickerController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderPickerController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var id = Int32.Parse(User.FindFirst("Id").Value);

        //    var response = await _orderService.GetOrders(id);

        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        return View(response.Data);
        //    }

        //    return View("Index");
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //this.HttpContext.Session["Student"] = "";
            var r = await _orderService.GetAllAwaitingOrders();
            var orders = new List<OrderDetailsViewModel>();
            if (r.StatusCode == System.Net.HttpStatusCode.OK)
            {
                foreach (var item in r.Data)
                {
                    var re = await _orderService.GetOrderDetails(item.OrderId);
                    orders.Add(re.Data);
                }
            }

            var response = new BaseResponse<IEnumerable<OrderDetailsViewModel>>()
            {
                Data = orders,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> Accept(Guid orderId)
        {
            _orderService.AcceptOrder(orderId);

            return RedirectToAction("Index", "OrderPicker");
        }

        [HttpGet]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            var response = await _orderService.GetOrderDetails(id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView(response.Data);
            }

            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(OrderDetailsViewModel model)
        {
            _orderService.CancelOrder(model);

            return RedirectToAction("Index", "OrderPicker");

            return RedirectToAction("Index", "Category");
        }

    }
}

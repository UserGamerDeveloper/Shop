using MicroElectronic.Domain.ViewModels.Account;
using MicroElectronic.Service.Implementations;
using MicroElectronic.Service.Interfaces;
using MicroElecWebStore.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MicroElectronic.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public AccountController(IAccountService accountService, IUserService userService, IOrderService orderService)
        {
            _accountService = accountService;
            _userService = userService;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    //return Redirect(returnUrl);
                    return RedirectToAction("Index", "Category");
                }

                ModelState.AddModelError("", response.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _accountService.Register(model);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Category");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Category");
        }

        public async Task<IActionResult> MyAccount() 
        {
            var id = Int32.Parse(User.FindFirst("Id").Value);

            var response = await _userService.GetUser(id);
            var orders = await _orderService.GetOrders(id);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewBag.Orders = orders.Data;
                return View(response.Data);
            }

            return View("MyAccount"); 
        }

        [HttpGet]
        public async Task<IActionResult> ShowOrderDetails(Guid orderId)
        {
            var response = await _orderService.GetOrderDetails(orderId);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView(response.Data);
            }

            return RedirectToAction("Index", "Category");
        }
    }
}

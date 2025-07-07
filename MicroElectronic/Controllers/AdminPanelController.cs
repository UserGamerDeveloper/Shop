using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.ViewModels.Order;
using MicroElectronic.Domain.ViewModels.Pagination;
using MicroElectronic.Domain.ViewModels.User;
using MicroElectronic.Service.Implementations;
using MicroElectronic.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroElectronic.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IUserService _userService;

        public AdminPanelController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var response = await _userService.GetAllUsers();
            
            int pageSize = 10;
            var count = response.Data.Count();
            var items = response.Data.Skip((page - 1) * pageSize)
                .Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexUsersViewModel indexViewModel = new IndexUsersViewModel()
            {
                PageViewModel = pageViewModel,
                Users = items
            };
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View("Index", indexViewModel);
            }

            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var response = await _userService.GetUser(id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView(response.Data);
            }

            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel model)
        {
            ModelState.Remove("Login");
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var newUser = new UserViewModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Position = model.Position,
                    Role = model.Role
                };

                var response = await _userService.Update(newUser);

                return RedirectToAction("Index", "AdminPanel");
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.GetUser(id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return PartialView(response.Data);
            }

            return RedirectToAction("Index", "Category");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(UserViewModel model)
        {
            //if (model.Id != 0)

                var response = await _userService.DeleteUser(model.Id);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "AdminPanel");
                }

            return RedirectToAction("Index", "Category");
        }
    }
}

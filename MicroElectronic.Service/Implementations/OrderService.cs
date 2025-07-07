using MicroElectronic.DAL.Interfaces;
using MicroElectronic.DAL.Repositories;
using MicroElectronic.Domain.Enum;
using MicroElectronic.Domain.Extensions;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.ApplicationItem;
using MicroElectronic.Domain.ViewModels.Order;
using MicroElectronic.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<OrderDetail> _orderDetailRepository;
        private readonly IBaseRepository<ApplicationItem> _applicationItemRepository;
        private readonly IBaseRepository<Equipment> _equipmentRepository;

        public OrderService(IBaseRepository<Order> orderRepository, IBaseRepository<OrderDetail> orderDetailRepository, 
            IBaseRepository<ApplicationItem> applicationItemRepository, IBaseRepository<Equipment> equipmentRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _applicationItemRepository = applicationItemRepository;
            _equipmentRepository = equipmentRepository;
        }

        public async Task<IBaseResponse<Order>> CreateOrder(OrderViewModel order)
        {
            try
            {
                var items = await _applicationItemRepository.GetAll().Where(a => a.UserId == order.UserId)
                    .Join(_equipmentRepository.GetAll(), a => a.EquipmentId, e => e.Id, (a, e) => new ApplicationItemViewModel
                    {
                        ItemId = a.Id,
                        EquipmentId = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Price = e.Price,
                        Quantity = a.Quantity,
                        ImageUrl = e.ImageUrl
                    }).ToListAsync();
                var newOrder = new Order()
                {
                    Id = order.OrderId,
                    UserId = order.UserId,
                    DateCreated = DateTime.Now,
                    Status = OrderStatus.Awaiting,
                    DateClosed = DateTime.MaxValue
                };
                await _orderRepository.Create(newOrder);

                foreach (var item in items)
                {
                    var newOrderDetails = new OrderDetail()
                    {
                        OrderId = newOrder.Id,
                        EquipmentId = item.EquipmentId,
                        Quantity = item.Quantity,
                        Price = item.Price * item.Quantity,
                    };
                    await _orderDetailRepository.Create(newOrderDetails);
                }

                return new BaseResponse<Order>()
                {
                    Data = newOrder,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[CreateOrder]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<OrderDetailsViewModel>> GetOrderDetails(Guid orderId)
        {
            try
            {
                var items = await _orderDetailRepository.GetAll().Where(od => od.OrderId == orderId)
                    .Join(_equipmentRepository.GetAll(), od => od.EquipmentId, e => e.Id, (od, e) => new ApplicationItemViewModel
                    {
                        EquipmentId = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Price = e.Price,
                        Quantity = od.Quantity,
                        ImageUrl = e.ImageUrl
                    }).ToListAsync();
                var details = await _orderDetailRepository.GetAll()
                    .Join(_orderRepository.GetAll(), od => od.OrderId, o => o.Id, (od, o) => new OrderDetailsViewModel()
                    {
                        UserId = o.UserId,
                        OrderId = orderId,
                        DateCreated = o.DateCreated,
                        DateClosed = o.DateClosed,
                        OrderItems = items
                    }).FirstOrDefaultAsync(_od => _od.OrderId == orderId);
                if (details == null)
                {
                    return new BaseResponse<OrderDetailsViewModel>()
                    {
                        Description = "Подробности заказа не найдены",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                return new BaseResponse<OrderDetailsViewModel>()
                {
                    Data = details,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderDetailsViewModel>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[GetOrderDetails]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetOrders(int userId)
        {
            try
            {
                var orders = await _orderRepository
                    .GetAll()
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(ob=>ob.DateCreated)
                    .Select(order => new OrderViewModel()
                    {
                        OrderId = order.Id,
                        UserId = order.UserId,
                        Status = order.Status.GetDisplayName(),
                        DateCreated = order.DateCreated,
                        DateClosed = order.DateClosed,
                    }).ToListAsync();

                if (!orders.Any())
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = "Заявки не найдены",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = orders,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[GetOrders]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetAllAwaitingOrders()
        {
            try
            {
                var orders = await _orderRepository
                    .GetAll()
                    .Where(x => x.Status == OrderStatus.Awaiting)
                    .OrderByDescending(ob => ob.DateCreated)
                    .Select(order => new OrderViewModel()
                    {
                        OrderId = order.Id,
                        UserId = order.UserId,
                        Status = order.Status.GetDisplayName(),
                        DateCreated = order.DateCreated,
                        DateClosed = order.DateClosed
                    }).ToListAsync();

                if (!orders.Any())
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = "Заявки не найдены",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = orders,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[GetOrders]: {ex.Message}"
                };
            }
        }
        public void AcceptOrder(Guid guid)
        {
            try
            {
                var orders = _orderRepository
                    .GetAll()
                    .Where(x => x.Id == guid)
                    .First();
                orders.Status = OrderStatus.Accepted;
                _orderRepository.Update(orders);
            }
            catch (Exception ex)
            {
                //return new BaseResponse<IEnumerable<OrderViewModel>>()
                //{
                //    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                //    Description = $"[GetOrders]: {ex.Message}"
                //}; 
            }
        }
        public async void CancelOrder(OrderDetailsViewModel model)
        {
            try
            {
                var orders = _orderRepository
                    .GetAll()
                    .Where(x => x.Id == model.OrderId)
                    .First();
                orders.Status = OrderStatus.Canceling;
                orders.Comment = model.Comment;
                await _orderRepository.Update(orders);
            }
            catch (Exception ex)
            {
                //return new BaseResponse<IEnumerable<OrderViewModel>>()
                //{
                //    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                //    Description = $"[GetOrders]: {ex.Message}"
                //}; CancelOrder
            }
        }

        public Task<IBaseResponse<Order>> UpdateStatus(int id, string status)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<bool>> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<OrderViewModel>> GetItemsList(int userId)
        {
            try
            {
                var items = await _applicationItemRepository.GetAll().Where(a => a.UserId == userId)
                    .Join(_equipmentRepository.GetAll(), a => a.EquipmentId, e => e.Id, (a, e) => new ApplicationItemViewModel
                    {
                        ItemId = a.Id,
                        EquipmentId = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Price = e.Price,
                        Quantity = a.Quantity,
                        ImageUrl = e.ImageUrl
                    }).ToListAsync();
                if (items.Count() == 0)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Элементы не найдены",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }

                var newViewModel = new OrderViewModel()
                {
                    OrderItems = items
                };
                return new BaseResponse<OrderViewModel>()
                {
                    Data = newViewModel,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderViewModel>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[GetItemsList]: {ex.Message}"
                };
            }
        }
    }
}

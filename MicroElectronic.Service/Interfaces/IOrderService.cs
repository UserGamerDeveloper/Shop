using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetOrders(int userId);

        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetAllAwaitingOrders();

        Task<IBaseResponse<OrderDetailsViewModel>> GetOrderDetails(Guid orderId);

        void AcceptOrder(Guid orderId);

        Task<IBaseResponse<Order>> CreateOrder(OrderViewModel order);

        Task<IBaseResponse<bool>> DeleteOrder(int id);

        Task<IBaseResponse<Order>> UpdateStatus(int id, string status);

        Task<IBaseResponse<OrderViewModel>> GetItemsList(int userId);
        void CancelOrder(OrderDetailsViewModel model);
    }
}

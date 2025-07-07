using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.ApplicationItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Service.Interfaces
{
    public interface IApplicationItemService
    {
        Task<IBaseResponse<ApplicationItem>> Add(int equipId, int userId);

        Task<IBaseResponse<bool>> Remove(int id);

        Task<IBaseResponse<bool>> Clear(int userId);

        Task<IBaseResponse<IEnumerable<ApplicationItemViewModel>>> GetApplicationItems(int userId);

        Task<IBaseResponse<bool>> ChangeQuantity(int id, string operation);
    }
}

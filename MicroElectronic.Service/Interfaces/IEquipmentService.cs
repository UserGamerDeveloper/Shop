using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Equipment;


namespace MicroElectronic.Service.Interfaces
{
    public interface IEquipmentService
    {
        Task<IBaseResponse<Equipment>> Create(EquipmentViewModel equipment);

        Task<IBaseResponse<IEnumerable<Equipment>>> GetEquipments(int categoryId);

        Task<IBaseResponse<EquipmentViewModel>> GetEquipment(int id);

        Task<IBaseResponse<Equipment>> Update(int id, EquipmentViewModel equipment);

        Task<IBaseResponse<bool>> Delete(int id);
    }
}

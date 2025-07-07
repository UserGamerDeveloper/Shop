using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Category;
using MicroElectronic.Domain.ViewModels.User;

namespace MicroElectronic.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<UserViewModel>>> GetAllUsers();

        Task<IBaseResponse<UserViewModel>> GetUser(int id);

        Task<IBaseResponse<User>> CreateUser(UserViewModel user);

        Task<IBaseResponse<bool>> DeleteUser(int id);

        Task<IBaseResponse<User>> Update(UserViewModel model);

        IBaseResponse<Dictionary<int, string>> SetRoles();
    }
}

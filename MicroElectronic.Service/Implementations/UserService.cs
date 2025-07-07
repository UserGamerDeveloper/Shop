using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Enum;
using MicroElectronic.Domain.Extensions;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Category;
using MicroElectronic.Domain.ViewModels.User;
using MicroElectronic.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<User>> CreateUser(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(user => user.Id == id);
                if(user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Description = "Пользователь не найден"
                    };
                }

                await _userRepository.Delete(user);
                return new BaseResponse<bool>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[DeleteUser]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAll().Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Name =x.Name,
                    Surname = x.Surname,
                    Position= x.Position,
                    Login = x.Login,
                    Role = x.Role.GetDisplayName()
                }).ToListAsync();

                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[GetAllUsers]: {ex.Message}"
                };
            }
        }

        public IBaseResponse<Dictionary<int, string>> SetRoles()
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<UserViewModel>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Description = "Пользователь не найден"
                    };
                }

                var data = new UserViewModel()
                {
                    Id=user.Id,
                    Name=user.Name,
                    Surname=user.Surname,
                    Position=user.Position,
                    Login=user.Login,
                    Role=user.Role.GetDisplayName()
                };

                return new BaseResponse<UserViewModel>()
                {
                    Data = data,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserViewModel>()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[GetUser]: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<User>> Update(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (user == null)
                {
                    return new BaseResponse<User>()
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Description = "Пользователь не найден"
                    };
                }

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Position = model.Position;
                user.Role = (Role) Enum.Parse(typeof(Role), model.Role);

                await _userRepository.Update(user);
                return new BaseResponse<User>()
                {
                    Data = user,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Description = $"[UpdateUser]: {ex.Message}"
                };
            }
        }
    }
}

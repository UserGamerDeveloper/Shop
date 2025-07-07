using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Enum;
using MicroElectronic.Domain.Helpers;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Account;
using MicroElectronic.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MicroElectronic.Service.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly IBaseRepository<User> _userRepository;

        public AccountService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже существует"
                    };
                }

                user = new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Position = model.Position,
                    Login = model.Login,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                    Role = Role.User
                };

                await _userRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Пользователь зарегистрирован",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином не найден"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                new Claim("UserName", user.Name),
                new Claim("UserSurname", user.Surname),
                new Claim("Id", user.Id.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}

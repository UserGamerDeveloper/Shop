using MicroElectronic.Domain.Response;
using MicroElectronic.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Service.Interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [StringLength(20, MinimumLength = 5, ErrorMessage ="Длина логина должна быть от 5 до 20 символов")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Введите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}

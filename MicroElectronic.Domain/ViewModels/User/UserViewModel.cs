using MicroElectronic.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Укажите фамилию пользователя")]
        public string Surname { get; set; } = "";

        //[Required(ErrorMessage = "Укажите должность пользователя")]
        public string Position { get; set; } = "";

        [Required(ErrorMessage = "Укажите логин пользователя")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль пользователя")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Укажите роль пользователя")]
        public string Role { get; set; }
    }
}

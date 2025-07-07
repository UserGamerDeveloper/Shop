using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите свое имя")]
        [MaxLength(50, ErrorMessage = "Длина поля должна быть меньше 50 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Укажите свою фамилию")]
        //[MaxLength(50, ErrorMessage = "Длина поля должна быть меньше 50 символов")]
        //[Display(Name = "Фамилия")]
        public string Surname { get; set; } = "";

        //[Required(ErrorMessage = "Укажите свою должность")]
        //[Display(Name = "Должность")]
        public string Position { get; set; } = "";

        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(20, ErrorMessage = "Логин должен быть меньше 20 символов")]
        [MinLength(5, ErrorMessage = "Логин должен быть больше 4 символов")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Логин должен содержать только латинские буквы и цифры")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен быть больше 6 символов")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Пароль должен содержать только латинские буквы и цифры")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }


    }
}

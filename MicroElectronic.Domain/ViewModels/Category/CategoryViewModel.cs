using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Category
{
    public class CategoryViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название категории")]
        [Required(ErrorMessage = "Укажите название категории")]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        public string ImageUrl { get; set; }
    }
}

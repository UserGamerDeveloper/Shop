using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.Enum
{
    public enum OrderStatus
    {
        [Display(Name = "В ожидании")]
        Awaiting = 0,

        [Display(Name = "Отменен")]
        Canceling = 1,

        [Display(Name = "Принят")]
        Accepted = 2,

        [Display(Name = "Выполнен")]
        Done = 3
    }
}

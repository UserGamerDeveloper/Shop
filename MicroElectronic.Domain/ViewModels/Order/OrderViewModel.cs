using MicroElectronic.Domain.Enum;
using MicroElectronic.Domain.Models;
using MicroElectronic.Domain.ViewModels.ApplicationItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Order
{
    public class OrderViewModel
    {
        public int UserId { get; set; }

        public Guid OrderId { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateClosed { get; set; }

        public string Status { get; set; }

        public List<ApplicationItemViewModel> OrderItems { get; set; }
    }
}

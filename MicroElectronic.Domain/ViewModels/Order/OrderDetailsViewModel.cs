using MicroElectronic.Domain.ViewModels.ApplicationItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public int UserId { get; set; }

        public Guid OrderId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateClosed { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public List<ApplicationItemViewModel> OrderItems { get; set; }
    }
}

using MicroElectronic.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime DateCreated { get; set; }

        public OrderStatus Status { get; set; }

        public string? Comment { get; set; }

        public DateTime DateClosed { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}

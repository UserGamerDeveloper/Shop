using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public int EquipmentId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}

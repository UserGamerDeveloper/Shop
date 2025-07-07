using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.Models
{
    public class ApplicationItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int Quantity { get; set; }

        public int EquipmentId { get; set; }

        public Equipment Equipment { get; set; }
    }
}

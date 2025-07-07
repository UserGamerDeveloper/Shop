using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.ApplicationItem
{
    public class ApplicationItemViewModel
    {
        public int ItemId { get; set; }

        public int EquipmentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity {get; set; }

        public string ImageUrl { get; set; }

        public bool IsSelected { get; set; }
    }
}

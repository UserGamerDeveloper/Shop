using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<MicroElectronic.Domain.Models.Equipment> Equipments { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

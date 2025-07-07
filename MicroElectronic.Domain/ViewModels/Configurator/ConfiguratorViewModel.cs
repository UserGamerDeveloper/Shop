using MicroElectronic.Domain.ViewModels.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Configurator
{
    public class ConfiguratorViewModel
    {
        public EquipmentViewModel Proccessor { get; set; }
        public EquipmentViewModel Motherboard { get; set; }
        public EquipmentViewModel Videocard { get; set; }
        public EquipmentViewModel RAM { get; set; }
        public EquipmentViewModel SSD { get; set; }
        public EquipmentViewModel Powerblock { get; set; }
    }
}

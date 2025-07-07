using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.Domain.ViewModels.Equipment
{
    public class EquipmentViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } = "";

        public decimal Price { get; set; }

        public string Size { get; set; } = "";

        public string BodyMaterial { get; set; } = "";

        public string WorkingArea { get; set; } = "";

        public int Power { get; set; }

        public string GuaranteePeriod { get; set; } = "";

        public string FullDescription { get; set; } = "";

        public string ImageUrl { get; set; }

        public int One { get; set; }

        public int Two { get; set; }

        public int Three { get; set; }

        public string ParamNameOne { get; set; } = "";
        public string ParamValueOne { get; set; } = "";
        public string ParamNameTwo { get; set; } = "";
        public string ParamValueTwo { get; set; } = "";
        public string ParamNameThree { get; set; } = "";
        public string ParamValueThree { get; set; } = "";
        public string ParamNameFour { get; set; } = "";
        public string ParamValueFour { get; set; } = "";
        public string ParamNameFive { get; set; } = "";
        public string ParamValueFive { get; set; } = "";
        public string ParamNameSix { get; set; } = "";
        public string ParamValueSix { get; set; } = "";
        public string ParamNameSeven { get; set; } = "";
        public string ParamValueSeven { get; set; } = "";
        public string ParamNameEight { get; set; } = "";
        public string ParamValueEight { get; set; } = "";
        public string ParamNameNine { get; set; } = "";
        public string ParamValueNine { get; set; } = "";
        public string ParamNameTen { get; set; } = "";
        public string ParamValueTen { get; set; } = "";
        public string ParamNameEleven { get; set; } = "";
        public string ParamValueEleven { get; set; } = "";
        public string ParamNameTwelve { get; set; } = "";
        public string ParamValueTwelve { get; set; } = "";
    }
}

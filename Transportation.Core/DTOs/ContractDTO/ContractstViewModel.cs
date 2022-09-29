﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation.Core.DTOs.ContractDTO
{
    public class CargoAnnounceViewModel
    {
        public int ContractorId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "نوع بار")]
        [MaxLength(300)]
        public string CargoType { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "نوع بسته بندی")]
        [MaxLength(300)]
        public string PackageType { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "تناژ بار")]
        public int CargoWeight { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "مبدا")]
        [MaxLength(400)]
        public string Beginning { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "مقصد")]
        [MaxLength(400)]
        public string Destination { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "صافی راننده")]
        public string DriverFee { get; set; }

        public List<int> TruckTypes { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "توضیحات")]
        [MaxLength(800)]
        public string Description { get; set; }
    }

    public class TruckTypesToSelect
    {
        public int TypeId { get; set; }

        public string TypeTitle { get; set; }
    }
}

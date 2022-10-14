using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Display(Name = "مبلغ پیشنهادی")]
        public string DriverFee { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "کامیون های درخواستی")]
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

    public class AnnounceViewModel
    {
        [Required]
        public int AnnounceId { get; set; }

        [Required]
        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "نوع بار")]
        [MaxLength(300)]
        public string CargoType { get; set; }

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
    }

    public class RequestViewModel
    {
        [Required]
        public int DriverId { get; set; }

        [Required]
        public int AnnounceId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "مبلغ درخواستی")]
        public int OfferPrice { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "توضیحات")]
        [MaxLength(800)]
        public string Description { get; set; }
    }
    public class ShowContractsViewModel
    {
        public int ContractId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "نوع بار")]
        [MaxLength(300)]
        public string CargoType { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "مبدا")]
        [MaxLength(400)]
        public string Beginning { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "مقصد")]
        [MaxLength(400)]
        public string Destination { get; set; }

        [Display(Name = "کد ناوگان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string TruckFleetCode { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "حواله دریافتی")]
        public int RecievedRemittance { get; set; }

        [Display(Name = "نام محول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "صافی راننده")]
        public int DriverFee { get; set; }

        [Required]
        public DateTime SignDate { get; set; }

        [Required]
        public DateTime DeleteDate { get; set; }
    }
}

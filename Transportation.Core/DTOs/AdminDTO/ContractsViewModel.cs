using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation.Core.DTOs.AdminDTO
{
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

        public DateTime SignDate { get; set; }
    }

    public class UsersToSelectList
    {
        public int UserId { get; set; }

        public string FullName { get; set; }
    }

    public class SignContractViewModel
    {
        public int DriverId { get; set; }

        public int ContractorId { get; set; }

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

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "صافی راننده")]
        public int DriverFee { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "حواله دریافتی")]
        public int RecievedRemittance { get; set; }

        public SelectList? Drivers { get; set; }

        public SelectList? Contractors { get; set; }
    }

    public class ContractsSelectLists
    {

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Entities.User;

namespace Transportation.DataLayer.Entities.Contract
{
    public class CargoAnnounce
    {
        [Key]
        public int AnnounceId { get; set; }

        [Required]
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

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "توضیحات")]
        [MaxLength(800)]
        public string Description { get; set; }

        #region Navigation Props

        public ICollection<CargoTruckType> CargoTruckTypes { get; set; }

        [ForeignKey("ContractorId")]
        public Contractor Contractor { get; set; }

        public List<ContractSign> Contracts { get; set; }

        public List<CargoRequest> Requests { get; set; }

        #endregion
    }
}

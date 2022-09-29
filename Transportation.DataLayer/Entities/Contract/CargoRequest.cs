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
    public class CargoRequest
    {
        [Key]
        public int RequestId { get; set; }

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

        #region Navigation Props

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

        [ForeignKey("AnnounceId")]
        public CargoAnnounce Announce { get; set; }

        #endregion
    }
}

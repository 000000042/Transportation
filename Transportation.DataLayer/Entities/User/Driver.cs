using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Entities.Contract;

namespace Transportation.DataLayer.Entities.User
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "کد هوشمند راننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string SmartDriverCode { get; set; }

        [Display(Name = "کد ناوگان کامیون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string TruckFleetCode { get; set; }

        [Required]
        public string IdentificationCard { get; set; }

        [Required]
        public string FacePicture { get; set; }

        [Required]
        public string SmartDriverCard { get; set; }

        #region Navigation Props

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<DriverTruck> DriverTrucks { get; set; }

        public List<CargoRequest> Requests { get; set; }

        #endregion
    }
}

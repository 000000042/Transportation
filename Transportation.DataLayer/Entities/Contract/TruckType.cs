using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Entities.User;

namespace Transportation.DataLayer.Entities.Contract
{
    public class TruckType
    {
        [Key]
        public int TypeId { get; set; }

        [Display(Name = "عنوان کامیون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string TypeTitle { get; set; }


        #region Navigation Props

        public ICollection<CargoTruckType> CargoTruckTypes { get; set; }

        public ICollection<DriverTruck> DriverTrucks { get; set; }

        #endregion
    }
}

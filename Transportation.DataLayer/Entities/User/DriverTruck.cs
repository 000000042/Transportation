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
    public class DriverTruck
    {
        [Key]
        public int DT_Id { get; set; }

        [Required]
        public int DriverId { get; set; }

        [Required]
        public int TruckId { get; set; }

        #region Navigation Props

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

        [ForeignKey("TruckId")]
        public TruckType TruckType { get; set; }

        #endregion
    }
}

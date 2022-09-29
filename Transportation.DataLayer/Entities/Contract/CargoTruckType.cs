using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation.DataLayer.Entities.Contract
{
    public class CargoTruckType
    {
        [Key]
        public int CT_Id { get; set; }

        public int AnnounceId { get; set; }

        public int TypeId { get; set; }


        #region Navigation Props

        [ForeignKey("TypeId")]
        public TruckType TruckType { get; set; }

        [ForeignKey("AnnounceId")]
        public CargoAnnounce CargoAnnounce { get; set; }

        #endregion
    }
}

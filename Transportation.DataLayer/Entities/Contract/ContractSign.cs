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
    public class ContractSign
    {
        [Key]
        public int ContractId { get; set; }

        public int AnnounceId { get; set; }

        public int RequestId { get; set; }

        public int AdminId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [Display(Name = "حواله دریافتی")]
        public int RecievedRemittance { get; set; }

        [Required]
        public DateTime SignDate { get; set; }

        [Required]
        public DateTime DeleteSignDate { get; set; }

        public bool IsDelete { get; set; }


        #region Navigation Props

        [ForeignKey("AdminId")]
        public User.Admin Admin { get; set; }

        [ForeignKey("AnnounceId")]
        public CargoAnnounce Announce { get; set; }

        [ForeignKey("RequestId")]
        public CargoRequest Request { get; set; }

        #endregion
    }
}

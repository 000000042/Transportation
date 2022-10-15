using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Entities.Contract;

namespace Transportation.DataLayer.Entities.User
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        public int UserId { get; set; }

        #region Navigation Props

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<ContractSign> AdminSigns { get; set; }

        #endregion
    }
}

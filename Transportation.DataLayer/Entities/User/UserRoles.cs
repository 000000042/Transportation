using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation.DataLayer.Entities.User
{
    public class UserRoles
    {
        [Key]
        public int UR_Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        #region Navigation Props

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        #endregion
    }
}

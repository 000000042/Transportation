using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Entities.User;

namespace Transportation.DataLayer.Entities.Permission
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }

        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        #region Navigation Props

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }

        #endregion
    }
}

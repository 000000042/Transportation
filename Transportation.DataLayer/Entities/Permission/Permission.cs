using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation.DataLayer.Entities.Permission
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        public int? ParentId { get; set; }

        [Display(Name = "نام دسترسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(200)]
        public string PermissionTitle { get; set; }

        #region Navigation Props

        public ICollection<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}

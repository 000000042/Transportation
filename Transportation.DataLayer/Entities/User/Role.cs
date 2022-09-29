using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Entities.Permission;

namespace Transportation.DataLayer.Entities.User
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }

        #region Navigation Props

        public ICollection<UserRoles> UserRoles { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.Security;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.Admin.Users.Drivers
{
    [Authorize]
    [PermissionChecker(3)]
    public class PendingUsersModel : PageModel
    {
        private IAdminService _adminService;

        public PendingUsersModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public List<ShowDriversViewModel> Users { get; set; }
        public void OnGet()
        {
            Users = _adminService.GetPendingDrivers();

            if (!Users.Any())
                ViewData["IsExistDrivers"] = false;
        }
    }
}

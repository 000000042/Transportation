using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.Security;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.Admin.Users.Drivers
{
    [AdminChecker(2)]
    public class PendingUsersModel : PageModel
    {
        private IAdminService _adminService;

        public PendingUsersModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public List<ShowUsersViewModel> Users { get; set; }
        public void OnGet()
        {
            Users = _adminService.GetPendingDrivers();
        }
    }
}

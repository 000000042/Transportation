using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.Admin
{
    public class LogOutModel : PageModel
    {
        private IAdminService _adminService;

        public LogOutModel(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult OnGet()
        {
            return Partial("_LogOutAdmin");
        }

        public IActionResult OnGetLogOutAdmin()
        {
            _adminService.LogOutAdmin();

            return Redirect("/Admin/Login");
        }
    }
}

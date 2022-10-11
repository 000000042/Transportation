using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.Admin.Users.Drivers
{
    public class CreateDriverModel : PageModel
    {
        private IAdminService _adminService;

        public CreateDriverModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public AddDriverUserViewModel User { get; set; }
        public IActionResult OnGet()
        {
            return Partial("_CreateDriverPartial", User);
        }

        public IActionResult OnPost(AddDriverUserViewModel driver)
        {
            if (!ModelState.IsValid)
                return Partial("_CreateDriverPartial", driver);

            int userId = _adminService.CreateDriverUserByAdmin(driver);
            return Redirect("/Admin/Users");
        }
    }
}

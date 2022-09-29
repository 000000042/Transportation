using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private IAdminService _adminService;

        public LoginModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public AdminLoginViewModel Admin { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Request.Cookies.Any(c => c.Key == "Admin.Auth"))
            {
                return Redirect("/Admin");
            }

            return Page();
        }

        public IActionResult OnPost(AdminLoginViewModel admin)
        {
            if (!ModelState.IsValid)
                return Page();

            if (_adminService.LoginAdmin(admin))
                return Redirect("/Admin");

            ModelState.AddModelError("UserNameOrEmail", "اطلاعات موجود نمی باشد!");
            return Page();
        }
    }
}

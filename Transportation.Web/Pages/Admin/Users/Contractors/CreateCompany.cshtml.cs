using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.Admin.Users.Contractors
{
    public class CreateCompanyModel : PageModel
    {
        private IAdminService _adminService;

        public CreateCompanyModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public AddContractorUserViewModel User { get; set; }
        public IActionResult OnGet()
        {
            return Partial("_CreateContractorPartial", User);
        }

        public IActionResult OnPost(AddContractorUserViewModel contractor)
        {
            if (!ModelState.IsValid)
                return Page();

            int userId = _adminService.CreateContractorUserByAdmin(contractor);
            return Redirect("/Admin/Users");
        }
    }
}

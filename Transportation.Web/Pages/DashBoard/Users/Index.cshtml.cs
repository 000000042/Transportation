using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.Enums.UserEnums;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private IAdminService _adminService;

        public IndexModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public List<ShowUsersViewModel> Users { get; set; }
        public void OnGet(string type)
        {
            if (type == null)
            {
                ViewData["Type"] = "All";
                Users = _adminService.GetUsersToShow();
            }
            if (type == UserTypes.Driver.ToString())
            {
                ViewData["Type"] = "Driver";
                Users = _adminService.GetDriversToShow();
            }
            else if (type == UserTypes.Contractor.ToString())
            {
                ViewData["Type"] = "Company";
                Users = _adminService.GetDriversToShow();
            }
            else
            {
                Redirect("/");
            }
        }
    }
}

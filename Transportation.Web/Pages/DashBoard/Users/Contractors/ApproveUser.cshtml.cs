using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.Conventors;
using Transportation.Core.Security;
using Transportation.Core.Senders;
using Transportation.Core.Services;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Web.Pages.Admin.Users.Contractors
{
    [PermissionChecker(16)]
    public class ApproveUserModel : PageModel
    {
        private IAdminService _adminService;
        private IViewRenderService _renderView;

        public ApproveUserModel(IAdminService adminService, IViewRenderService renderView)
        {
            _adminService = adminService;
            _renderView = renderView;
        }

        public Contractor User { get; set; }
        public IActionResult OnGet(int id)
        {
            User = _adminService.GetContractor(id);
            if (User.User.IsDelete || User.User.IsActive)
                return BadRequest();

            return Partial("_ApproveContractorPartial", User);
        }

        public IActionResult OnGetDeclineUser(int id)
        {
            bool declineUser = _adminService.DeleteUser(id);
            if (!declineUser)
                return BadRequest();

            User user = _adminService.GetUser(id);
            string emailView = _renderView.RenderToStringAsync("_UserApproveResult", user);
            SendEmail.Send(user.Email, "تاییدیه حساب کاربری", emailView);
            return RedirectToPage("PendingUsers");
        }

        public IActionResult OnGetAcceptUser(int id)
        {
            bool activeUser = _adminService.ActiveUser(id);
            if (!activeUser)
                return BadRequest();


            User user = _adminService.GetUser(id);
            string emailView = _renderView.RenderToStringAsync("_UserApproveResult", user);
            SendEmail.Send(user.Email, "تاییدیه حساب کاربری", emailView);
            return RedirectToPage("PendingUsers");
        }
    }
}

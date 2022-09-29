using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Transportation.Core.Conventors;
using Transportation.Core.Security;
using Transportation.Core.Senders;
using Transportation.Core.Services;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Web.Pages.Admin.Users.Drivers
{
    public class ApproveUserModel : PageModel
    {
        private IAdminService _adminService;
        private IViewRenderService _renderView;

        public ApproveUserModel(IAdminService adminService, IViewRenderService renderView)
        {
            _adminService = adminService;
            _renderView = renderView;
        }

        public Driver User { get; set; }
        public IActionResult OnGet(int id)
        {
            User = _adminService.GetDriver(id);
            if (User.User.IsDelete || User.User.IsActive)
                return BadRequest();

            return Partial("_ApproveDriverPartial", User);
        }

        public IActionResult OnGetDeclineUser(int id)
        {
            bool declineUser =  _adminService.DeleteUser(id);
            if (!declineUser)
                return BadRequest();

            User user = _adminService.GetUser(id);
            string emailView = _renderView.RenderToStringAsync("_UserApproveResult", user);
            SendEmail.Send(user.Email, "تاییدیه حساب کاربری", emailView);
            return RedirectToPage("PendingUsers");
        }

        public IActionResult OnGetAcceptUser(int id)
        {
            bool activeeUser = _adminService.ActiveUser(id);
            if (!activeeUser)
                return BadRequest();

            User user = _adminService.GetUser(id);
            string emailView = _renderView.RenderToStringAsync("_UserApproveResult", user);
            SendEmail.Send(user.Email, "تاییدیه حساب کاربری", emailView);
            return RedirectToPage("PendingUsers");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.Security;

namespace Transportation.Web.Pages.Admin.Contracts
{
    [Authorize]
    [PermissionChecker(1004)]
    public class DeleteContractModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

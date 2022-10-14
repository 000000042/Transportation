using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.Security;

namespace Transportation.Web.Pages.Admin.Contracts
{
    [PermissionChecker(1004)]
    public class DeleteContractModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

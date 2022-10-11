using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Transportation.Web.Pages.DashBoard.Contracts
{
    public class RequestCargoModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Partial("_RequestCargoPartial");
        }
    }
}

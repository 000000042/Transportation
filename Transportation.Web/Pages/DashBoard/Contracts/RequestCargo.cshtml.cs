using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.Core.Security;
using Transportation.Core.Services;
using Transportation.DataLayer.Entities.Contract;

namespace Transportation.Web.Pages.DashBoard.Contracts
{
    [Authorize]
    [PermissionChecker(19)]
    public class RequestCargoModel : PageModel
    {
        private IContractService _contractService;

        public RequestCargoModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        public RequestViewModel Request { get; set; }
        public IActionResult OnGet(int announceId)
        {
            Request = new RequestViewModel()
            {
                AnnounceId = announceId
            };
            return Partial("_RequestCargoPartial", Request);
        }

        public IActionResult OnPost(RequestViewModel request)
        {
            if (!ModelState.IsValid)
                return Page();

            
            _contractService.AddRequestToAnnounce(request);
            return Redirect("/DashBoard");
        }
    }
}

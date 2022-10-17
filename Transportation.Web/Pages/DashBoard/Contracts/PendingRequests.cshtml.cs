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
    [PermissionChecker(1006)]
    public class PendingRequestsModel : PageModel
    {
        private IContractService _contractService;

        public PendingRequestsModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        [BindProperty]
        public List<CargoRequest> Requests { get; set; }
        public IActionResult OnGet(int announceId)
        {
            if (!_contractService.IsExistAnnounce(announceId))
                return NotFound();

            if (!_contractService.IsExistRequests(announceId))
                ViewData["IsExistRequests"] = false;

            if (_contractService.IsExistContractAnnounce(announceId))
                ViewData["IsContractSigned"] = true;

            Requests = _contractService.GetRequestsForAnnounce(announceId);
            ViewData["AnnounceId"] = announceId;
            return Page();
        }

        public IActionResult OnGetAcceptOffer(int requestId)
        {
            int contractId = _contractService.SignContract(requestId);
            if (contractId == 0)
                return NotFound();

            return Redirect("/DashBoard/Contracts/Announces");
        }

        public IActionResult OnGetDeclineOffer(int requestId)
        {
            bool declineOffer = _contractService.DeclineRequest(requestId);
            if (!declineOffer)
                return NotFound();

            int announceId = Requests.SingleOrDefault(r => r.RequestId == requestId).AnnounceId;
            return Redirect("/DashBoard/Contracts/PendingRequests/" + announceId);
        }
    }
}

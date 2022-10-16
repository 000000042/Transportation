using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.Core.Security;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.DashBoard.Contracts
{
    [PermissionChecker(1, 17, 18, 1002)]
    public class AnnouncesModel : PageModel
    {
        private IContractService _contractService;

        public AnnouncesModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        public List<AnnounceViewModel> Announces { get; set; }
        public void OnGet()
        {
            Announces = _contractService.GetAnnouncesToShow();
        }
    }
}

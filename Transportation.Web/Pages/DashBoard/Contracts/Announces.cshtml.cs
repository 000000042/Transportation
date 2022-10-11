using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.DashBoard.Contracts
{
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

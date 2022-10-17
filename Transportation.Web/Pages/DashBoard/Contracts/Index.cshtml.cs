using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.Core.Security;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.Admin.Contracts
{
    [Authorize]
    [PermissionChecker(1002)]
    public class IndexModel : PageModel
    {
        private IContractService _contractService;

        public IndexModel(IContractService contractService)
        {
            _contractService = contractService;    
        }

        public List<ShowContractsViewModel> Contracts { get; set; }
        public void OnGet()
        {
            Contracts = _contractService.GetContractsForShow();

            if (!Contracts.Any())
                ViewData["IsExistContracts"] = false;
        }
    }
}

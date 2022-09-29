using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Transportation.Core.Services;
using Transportation.DataLayer.Entities.Contract;

namespace Transportation.Web.Pages.Admin.Contracts
{
    public class ShowContractModel : PageModel
    {
        private IContractService _contractService;

        public ShowContractModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        public ContractSign Contract { get; set; }
        public IActionResult OnGet(int id)
        {
            Contract = _contractService.GetContractToShow(id);

            return Partial("_ShowContractPartial", Contract);
        }
    }
}

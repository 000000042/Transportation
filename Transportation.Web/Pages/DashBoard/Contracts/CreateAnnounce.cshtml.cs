using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.Core.Security;
using Transportation.Core.Services;

namespace Transportation.Web.Pages.DashBoard.Contracts
{
    [Authorize]
    [PermissionChecker(20)]
    public class CreateAnnounceModel : PageModel
    {
        private IContractService _contractService;
        private IAccountService _accountService;

        public CreateAnnounceModel(IContractService contractService, IAccountService accountService)
        {
            _contractService = contractService;
            _accountService = accountService;
        }

        [BindProperty]
        public CargoAnnounceViewModel Announce { get; set; }
        public void OnGet()
        {
            ViewData["ContractorId"] = _accountService.GetContractorIdByUserName(User.Identity.Name);
            var types = new List<SelectListItem>();
            types.AddRange(_contractService.GetTruckTypesToSelect());
            ViewData["Types"] = new SelectList(types, "Value", "Text");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            int announceId = _contractService.AddCargoAnnounce(Announce);
            ViewData["IsSuccess"] = true;
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.Services;
using Transportation.DataLayer.Entities.Contract;

namespace Transportation.Web.Pages.Admin.Contracts
{
    public class AddContractModel : PageModel
    {
        private IContractService _contractService;

        public AddContractModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        public SignContractViewModel Contract { get; set; }
        public IActionResult OnGet()
        {
            Contract = new SignContractViewModel();

            List<SelectListItem> drivers = new List<SelectListItem>()
            {
                                new SelectListItem()
                {
                    Text = "انتخاب کنید",
                    Value = "0"
                }
            };
            drivers.AddRange(_contractService.GetDriversAsItems());
            Contract.Drivers = new SelectList(drivers, "Value", "Text");

            List<SelectListItem> contractors = new List<SelectListItem>()
            {
                                new SelectListItem()
                {
                    Text = "انتخاب کنید",
                    Value = "0"
                }
            };
            contractors.AddRange(_contractService.GetContractorsAsItems());
            Contract.Contractors = new SelectList(contractors, "Value", "Text");

            return Partial("_AddContractPartial", Contract);
        }

        public IActionResult OnPostSignContract(SignContractViewModel sign)
        {
            if (!ModelState.IsValid)
                return Page();

            
            int contractId = _contractService.SignContract(sign);
            return Redirect("/Admin/Contracts");
        }
    }
}

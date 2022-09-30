using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.Core.Enums.UserEnums;
using Transportation.Core.Security;
using Transportation.Core.Services;
using Transportation.DataLayer.Entities.Contract;

namespace Transportation.Web.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private IContractService _contractService;
        private IAccountService _accountService;

        public ContractController(IContractService contractService, IAccountService accountService)
        {
            _contractService = contractService;
            _accountService = accountService;
        }

        [Route("CargoList")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("CreateCargoAnnounce")]
        public IActionResult CreateCargoAnnounce()
        {
            ViewBag.ContractorId = _accountService.GetContractorIdByUserName(User.Identity.Name);
            var types = new List<SelectListItem>();
            types.AddRange(_contractService.GetTruckTypesToSelect());
            ViewData["Types"] = new SelectList(types, "Value", "Text");
            return View();
        }

        [Route("CreateCargoAnnounce")]
        [HttpPost]
        public IActionResult CreateCargoAnnounce(CargoAnnounceViewModel announce)
        {
            if (!ModelState.IsValid)
                return View(announce);

            int announceId = _contractService.AddCargoAnnounce(announce);
            return Redirect("/");
        }

        [Route("ShowAnnounce/{announceId}")]
        public IActionResult ShowAnnounce(int announceId)
        {
            CargoAnnounce announce = _contractService.GetCargoAnnounceById(announceId);
            if (announce == null)
                return NotFound();

            return View(announce);
        }
    }
}

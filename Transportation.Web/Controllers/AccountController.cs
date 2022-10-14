using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using Transportation.Core.Convertors;
using Transportation.Core.DTOs.AccountDTO;
using Transportation.Core.Enums.UserEnums;
using Transportation.Core.Services;

namespace Transportation.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private IContractService _contractService;

        public AccountController(IAccountService accountService, IContractService contractService)
        {
            _accountService = accountService;
            _contractService = contractService;
        }

        [Route("Login")]
        public IActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(UserLoginViewModel login, string ReturnUrl)
        {
            if (!ModelState.IsValid)
                return View("Login", login);

            if (_accountService.LoginUser(login))
                ViewBag.IsSuccess = true;

            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return LocalRedirect(ReturnUrl);
            }

            return View("Login", login);
        }

        [Route("LogOut")]
        public IActionResult LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                _accountService.LogOutUser();
                return Redirect("/");
            }

            return Redirect("/Login");
        }

        [Route("Register/Driver")]
        public IActionResult RegisterDriver()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            var types = new List<SelectListItem>();
            types.AddRange(_contractService.GetTruckTypesToSelect());
            ViewData["Types"] = new SelectList(types, "Value", "Text");
            return View("RegisterDriver");
        }

        [Route("Register/Driver")]
        [HttpPost]
        public IActionResult RegisterDriver(DriverRegisterViewModel driver)
        {
            if (!ModelState.IsValid)
                return View(driver);

            if (!driver.IsAccept)
            {
                ModelState.AddModelError("IsAccept", "ابتدا باید با مفاد شرایط و قوانین موافقت کنید!");
                return View("RegisterDriver", driver);
            }
            int driverId = _accountService.RegisterDriver(driver);
            return View("_SuccessRegister");
        }

        [Route("Register/Contractor")]
        public IActionResult RegisterContractor()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");

            return View("RegisterContractor");
        }

        [Route("Register/Contractor")]
        [HttpPost]
        public IActionResult RegisterContractor(ContractorRegisterViewModel contractor)
        {
            if (!ModelState.IsValid)
                return View(contractor);

            int companyId = _accountService.RegisterContractor(contractor);
            return View("_SuccessRegister");
        }
    }
}

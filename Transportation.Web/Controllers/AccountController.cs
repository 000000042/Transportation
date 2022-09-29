using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transportation.Core.Convertors;
using Transportation.Core.DTOs.AccountDTO;
using Transportation.Core.Enums.UserEnums;
using Transportation.Core.Services;

namespace Transportation.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginUser(UserLoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View("Login", login);

            if (_accountService.LoginUser(login))
                ViewBag.IsSuccess = true;
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

        [Route("Register/{type?}")]
        public IActionResult Register(string type)
        {
            if (type != null)
            {
                if (type == UserTypes.Driver.ToString())
                {
                    return View("RegisterDriver");
                }
                if (type == UserTypes.Contractor.ToString())
                {
                    return View("RegisterContractor");
                }
                else
                {
                    return NotFound();
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult RegisterDriver(DriverRegisterViewModel driver)
        {
            if (!ModelState.IsValid)
                return View(driver);

            int driverId = _accountService.RegisterDriver(driver);
            return View("_SuccessRegister");
        }

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

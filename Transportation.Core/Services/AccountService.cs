using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AccountDTO;
using Transportation.Core.Enums.UserEnums;
using Transportation.Core.Generators;
using Transportation.Core.Repositories;
using Transportation.Core.Security;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;
        private HttpContextAccessor _context;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _context = new HttpContextAccessor();
        }

        public bool LoginUser(UserLoginViewModel login)
        {
            if (_accountRepository.IsExistEmail(login.Email))
            {
                User user = _accountRepository.GetUser(_accountRepository.GetUserIdByUsernameOrEmail(login.Email));
                if (user != null)
                {
                    var salt = user.Salt;
                    string fixedPassword = PasswordHelper.EncodePassword(login.Password, salt);

                    if (IsPasswordCurrect(login.Email, fixedPassword))
                    {
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email)
                        };

                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        AuthenticationProperties properties = new AuthenticationProperties()
                        {
                            IsPersistent = login.RememberMe,
                            AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                        };

                        _context.HttpContext.SignInAsync(principal, properties);


                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }

            return false;
        }

        public int RegisterContractor(ContractorRegisterViewModel register)
        {
            string userName = register.FirstName + " " + register.LastName;

            if (_accountRepository.IsExistEmail(register.Email))
                return 0;

            if (_accountRepository.IsExistUserName(userName))
                return 0;

            if (_accountRepository.IsExistPhoneNumber(register.PhoneNumber))
                return 0;

            var salt = PasswordHelper.SaltGenerator();

            User user = new User()
            {
                ActiveCode = GuidGenerator.GuidGenerate(),
                UserName = userName,
                Email = register.Email,
                Password = PasswordHelper.EncodePassword(register.Password, salt),
                IsActive = false,
                IsDelete = false,
                Salt = salt,
                UserType = UserTypes.Contractor.ToString(),
                RegisterDate = DateTime.Now
            };
            int userId = _accountRepository.AddUser(user);

            Contractor contractor = new Contractor()
            {
                PhoneNumber = register.PhoneNumber,
                FirstName = register.FirstName,
                LastName = register.LastName,
                FacePicture = GuidGenerator.GuidGenerate() + Path.GetExtension(register.FacePicture.FileName),
                NationalCode = register.NationalCode,

                IdentificationCard = GuidGenerator.GuidGenerate() + Path.GetExtension(register.IdentificationCard.FileName),
                UserId = userId
            };


            _accountRepository.AddContractor(contractor);

            UserRoles addRoles = new UserRoles()
            {
                UserId = user.UserId,
                RoleId = 4
            };

            _accountRepository.AddRoleToUser(addRoles);

            string cardFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Contractors",
                "IdentificationCards",
                contractor.IdentificationCard);

            string facePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Contractors",
                "FacePictures",
                contractor.FacePicture);

            using (var stream = new FileStream(cardFilePath, FileMode.Create))
            {
                register.IdentificationCard.CopyTo(stream);
            }

            using (var stream = new FileStream(facePictureFilePath, FileMode.Create))
            {
                register.FacePicture.CopyTo(stream);
            }

            return userId;
        }

        public int RegisterDriver(DriverRegisterViewModel register)
        {
            string userName = register.FirstName + " " + register.LastName;
            if (_accountRepository.IsExistEmail(register.Email))
                return 0;

            if (_accountRepository.IsExistUserName(userName))
                return 0;

            if (_accountRepository.IsExistPhoneNumber(register.PhoneNumber))
                return 0;

            var salt = PasswordHelper.SaltGenerator();
            User user = new User()
            {
                ActiveCode = GuidGenerator.GuidGenerate(),
                UserName = userName,
                Email = register.Email,
                Password = PasswordHelper.EncodePassword(register.NationalCode, salt),
                IsActive = false,
                IsDelete = false,
                Salt = salt,
                UserType = UserTypes.Driver.ToString(),
                RegisterDate = DateTime.Now
            };
            int userId = _accountRepository.AddUser(user);

            Driver driver = new Driver()
            {
                PhoneNumber = register.PhoneNumber,
                FirstName = register.FirstName,
                LastName = register.LastName,
                TruckType = register.TruckType,
                TruckFleetCode = register.TruckFleetCode,
                NationalCode = register.NationalCode,
                SmartDriverCode = register.SmartDriverCode,
                IdentificationCard = GuidGenerator.GuidGenerate() + Path.GetExtension(register.IdentificationCard.FileName),
                FacePicture = GuidGenerator.GuidGenerate() + Path.GetExtension(register.FacePicture.FileName),
                UserId = userId
            };

            _accountRepository.AddDriver(driver);

            UserRoles addRoles = new UserRoles()
            {
                UserId = user.UserId,
                RoleId = 3
            };

            _accountRepository.AddRoleToUser(addRoles);

            string cardFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Drivers",
                "IdentificationCards",
                driver.IdentificationCard);

            string facePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Drivers",
                "FacePictures",
                driver.FacePicture);

            using (var stream = new FileStream(cardFilePath, FileMode.Create))
            {
                register.IdentificationCard.CopyTo(stream);
            }

            using (var stream = new FileStream(facePictureFilePath, FileMode.Create))
            {
                register.FacePicture.CopyTo(stream);
            }

            return userId;
        }

        public bool IsPasswordCurrect(string userNameOrEmail, string password)
        {
            User user = _accountRepository.GetUser(_accountRepository.GetUserIdByUsernameOrEmail(userNameOrEmail));

            if (password != user.Password)
                return false;

            return true;
        }

        public void LogOutUser()
        {
            _context.HttpContext.SignOutAsync();
        }

        public bool CheckUserPermissions(int permissionId, string userName)
        {
            if (_accountRepository.CheckUserPermissions(permissionId, userName))
                return true;

            return false;
        }

        public int GetUserIdByEmail(string email)
        {
            return _accountRepository.GetUserIdByEmail(email);
        }

        public int GetDriverIdByUserName(string userName)
        {
            return _accountRepository.GetDriverIdByUserName(userName);
        }

        public int GetContractorIdByUserName(string userName)
        {
            return _accountRepository.GetContractorIdByUserName(userName);
        }
    }
}

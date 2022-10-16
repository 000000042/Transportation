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
            if (_accountRepository.IsExistPhoneNumber(login.PhoneNumber))
            {
                User user = _accountRepository.GetUser(_accountRepository.GetUserIdByPhoneNumber(login.PhoneNumber));
                if (user != null)
                {
                    var salt = user.Salt;
                    string fixedPassword = PasswordHelper.EncodePassword(login.Password, salt);

                    if (IsPasswordCurrect(user.UserName, fixedPassword))
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
                FirstName = register.FirstName,
                LastName = register.LastName,
                NationalCode = register.NationalCode,
                PhoneNumber = register.PhoneNumber,
                Password = PasswordHelper.EncodePassword(register.NationalCode, salt),
                IsActive = false,
                IsDelete = false,
                Salt = salt,
                UserType = UserTypes.Contractor.ToString(),
                RegisterDate = DateTime.Now,
                FacePicture = GuidGenerator.GuidGenerate() + Path.GetExtension(register.FacePicture.FileName),
                IdentificationCard = GuidGenerator.GuidGenerate() + Path.GetExtension(register.IdentificationCard.FileName),
            };
            int userId = _accountRepository.AddUser(user);

            Contractor contractor = new Contractor()
            {
                UserId = userId
            };


            _accountRepository.AddContractor(contractor);

            UserRole addRoles = new UserRole()
            {
                UserId = user.UserId,
                RoleId = 5
            };

            _accountRepository.AddRoleToUser(addRoles);

            string cardFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Contractors",
                "IdentificationCards",
                user.IdentificationCard);

            string facePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Contractors",
                "FacePictures",
                user.FacePicture);

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
                FirstName = register.FirstName,
                LastName = register.LastName,
                NationalCode = register.NationalCode,
                PhoneNumber = register.PhoneNumber,
                Password = PasswordHelper.EncodePassword(register.NationalCode, salt),
                IsActive = false,
                IsDelete = false,
                Salt = salt,
                UserType = UserTypes.Driver.ToString(),
                RegisterDate = DateTime.Now,
                IdentificationCard = GuidGenerator.GuidGenerate() + Path.GetExtension(register.IdentificationCard.FileName),
                FacePicture = GuidGenerator.GuidGenerate() + Path.GetExtension(register.FacePicture.FileName)
            };
            int userId = _accountRepository.AddUser(user);

            Driver driver = new Driver()
            {
                TruckFleetCode = register.TruckFleetCode,
                SmartDriverCode = register.SmartDriverCode,
                SmartDriverCard = GuidGenerator.GuidGenerate() + Path.GetExtension(register.SmartDriverCard.FileName),
                UserId = userId
            };

            _accountRepository.AddDriver(driver);

            UserRole addRoles = new UserRole()
            {
                UserId = user.UserId,
                RoleId = 6
            };

            foreach(var truck in register.TruckTypes)
            {
                DriverTruck driverTruck = new DriverTruck()
                {
                    DriverId = driver.DriverId,
                    TruckId = truck
                };

                _accountRepository.AddTruckTypesToDriver(driverTruck);
            }

            _accountRepository.AddRoleToUser(addRoles);

            string cardFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Drivers",
                "IdentificationCards",
                user.IdentificationCard);

            string facePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Drivers",
                "FacePictures",
                user.FacePicture);

            string smartDriverCardPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Drivers",
                "SmartDriverCards",
                driver.SmartDriverCard);

            using (var stream = new FileStream(cardFilePath, FileMode.Create))
            {
                register.IdentificationCard.CopyTo(stream);
            }

            using (var stream = new FileStream(facePictureFilePath, FileMode.Create))
            {
                register.FacePicture.CopyTo(stream);
            }

            using (var stream = new FileStream(smartDriverCardPath, FileMode.Create))
            {
                register.SmartDriverCard.CopyTo(stream);
            }

            return userId;
        }

        public bool IsPasswordCurrect(string userNameOrEmail, string password)
        {
            User user = _accountRepository.GetUser(_accountRepository.GetUserIdByUserName(userNameOrEmail));

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

        public int GetDriverIdByUserName(string userName)
        {
            return _accountRepository.GetDriverIdByUserName(userName);
        }

        public int GetContractorIdByUserName(string userName)
        {
            return _accountRepository.GetContractorIdByUserName(userName);
        }

        public int GetAdminIdByUserName(string userName)
        {
            return _accountRepository.GetAdminIdByUserName(userName);
        }

        public bool CheckUserRoles(int roleId, string userName)
        {
            return _accountRepository.CheckUserRoles(roleId, userName);
        }
    }
}

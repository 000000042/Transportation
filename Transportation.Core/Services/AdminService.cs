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
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.Enums.UserEnums;
using Transportation.Core.Generators;
using Transportation.Core.Repositories;
using Transportation.Core.Security;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Services
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        private HttpContextAccessor _context;
        private IConfiguration _configuration;
        private IAccountRepository _accountRepository;

        public AdminService(IAdminRepository adminRepository,
            IConfiguration configuration, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _adminRepository = adminRepository;
            _context = new HttpContextAccessor();
            _accountRepository = accountRepository;
        }

        public bool ActiveUser(int userId)
        {
            User user = _adminRepository.GetUser(userId);
            if (user == null)
                return false;

            if (user.IsActive)
                return false;

            user.IsActive = true;
            _adminRepository.UpdateUser(user);

            return true;
        }

        public bool AddRoleToUser(int userId, int roleId)
        {
            User user = _adminRepository.GetUser(userId);
            if (user == null)
                return false;

            if (_adminRepository.IsRoleExist(roleId, userId))
                return false;

            UserRole addRole = new UserRole()
            {
                RoleId = roleId,
                UserId = userId
            };
            _adminRepository.GiveRoleToUser(addRole);

            return true;
        }

        public bool CheckUserPermissions(int permissionId, string email)
        {
            if (_adminRepository.CheckUserPermissions(permissionId, email))
                return true;

            return false;
        }

        public int CreateContractorUserByAdmin(AddContractorUserViewModel company)
        {
            if (company == null)
                return 0;

            string userName = company.FirstName + " " + company.LastName;

            if (_accountRepository.IsExistEmail(company.Email))
                return 0;

            if (_accountRepository.IsExistUserName(userName))
                return 0;

            if (_accountRepository.IsExistPhoneNumber(company.PhoneNumber))
                return 0;

            var salt = PasswordHelper.SaltGenerator();
            User user = new User()
            {
                UserName = userName,
                Password = PasswordHelper.EncodePassword(company.NationalCode, salt),
                Salt = salt,
                ActiveCode = GuidGenerator.GuidGenerate(),
                Email = company.Email,
                RegisterDate = DateTime.Now,
                IsActive = true,
                IsDelete = false,
                UserType = UserTypes.Driver.ToString()
            };
            int userId = _accountRepository.AddUser(user);

            Contractor newContractor = new Contractor()
            {
                UserId = userId,
                IdentificationCard = GuidGenerator.GuidGenerate() + Path.GetExtension(company.IdentificationCard.FileName),
                FacePicture = GuidGenerator.GuidGenerate() + Path.GetExtension(company.FacePicture.FileName)
            };
            int driverId = _accountRepository.AddContractor(newContractor);

            UserRole roles = new UserRole()
            {
                UserId = userId,
                RoleId = 4
            };
            _adminRepository.GiveRoleToUser(roles);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Companies",
                "IdentificationCards",
                newContractor.IdentificationCard);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                company.IdentificationCard.CopyTo(stream);
            }

            return userId;
        }

        public int CreateDriverUserByAdmin(AddDriverUserViewModel driver)
        {
            if (driver == null)
                return 0;

            string userName = driver.FirstName + " " + driver.LastName;

            if (_accountRepository.IsExistEmail(driver.Email))
                return 0;

            if (_accountRepository.IsExistUserName(userName))
                return 0;

            if (_accountRepository.IsExistPhoneNumber(driver.PhoneNumber))
                return 0;

            var salt = PasswordHelper.SaltGenerator();
            User user = new User()
            {
                UserName = userName,
                Password = PasswordHelper.EncodePassword(driver.NationalCode, salt),
                Salt = salt,
                ActiveCode = GuidGenerator.GuidGenerate(),
                Email = driver.Email,
                RegisterDate = DateTime.Now,
                IsActive = true,
                IsDelete = false,
                UserType = UserTypes.Driver.ToString()
            };
            int userId = _accountRepository.AddUser(user);

            Driver newDriver = new Driver()
            {
                TruckFleetCode = driver.TruckFleetCode,
                SmartDriverCode = driver.SmartDriverCode,
                UserId = userId,
                IdentificationCard = GuidGenerator.GuidGenerate() + Path.GetExtension(driver.IdentificationCard.FileName)
            };

            int driverId = _accountRepository.AddDriver(newDriver);

            UserRole roles = new UserRole()
            {
                UserId = userId,
                RoleId = 3
            };
            _adminRepository.GiveRoleToUser(roles);


            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "UserContent",
                "Drivers",
                "IdentificationCards",
                newDriver.IdentificationCard);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                driver.IdentificationCard.CopyTo(stream);
            }

            return userId;
        }

        public bool DeleteUser(int userId)
        {
            User user = _adminRepository.GetUser(userId);
            if (user == null)
                return false;

            if (user.IsDelete)
                return false;

            user.IsDelete = true;
            _adminRepository.UpdateUser(user);

            return true;
        }

        public List<ShowUsersViewModel> GetContractorsToShow()
        {
            List<Contractor> companies = _adminRepository.GetContractorsForShow();
            List<ShowUsersViewModel> companyUsers = companies
                .Where(c => c.User.IsActive == true)
                .Select(c => new ShowUsersViewModel
                {
                    UserName = c.User.UserName,
                    Email = c.User.Email,
                    RegisterDate = c.User.RegisterDate,
                    UserId = c.UserId,
                    UserType = UserTypes.Contractor.ToString()
                }).ToList();

            return companyUsers;
        }

        public Contractor GetContractor(int userId)
        {
            return _adminRepository.GetContractor(userId);
        }

        public Driver GetDriver(int userId)
        {
            return _adminRepository.GetDriver(userId);
        }

        public List<ShowUsersViewModel> GetDriversToShow()
        {
            List<Driver> drivers = _adminRepository.GetDriversForShow();
            List<ShowUsersViewModel> driverUsers = drivers
                .Where(c => c.User.IsActive == true)
                .Select(c => new ShowUsersViewModel
                {
                    UserName = c.User.UserName,
                    Email = c.User.Email,
                    RegisterDate = c.User.RegisterDate,
                    UserId = c.UserId,
                    UserType = UserTypes.Contractor.ToString()
                }).ToList();

            return driverUsers;
        }

        public List<ShowUsersViewModel> GetPendingContractors()
        {
            List<Contractor> contractors = _adminRepository.GetContractorsForShow();
            List<ShowUsersViewModel> contractorUsers = contractors
                .Where(c => c.User.IsActive == false)
                .Select(c => new ShowUsersViewModel
                {
                    UserName = c.User.UserName,
                    Email = c.User.Email,
                    RegisterDate = c.User.RegisterDate,
                    UserId = c.UserId,
                    UserType = UserTypes.Contractor.ToString()
                }).ToList();

            return contractorUsers;
        }

        public List<ShowUsersViewModel> GetPendingDrivers()
        {
            List<Driver> drivers = _adminRepository.GetDriversForShow();
            List<ShowUsersViewModel> driverUsers = drivers
                .Where(d => !d.User.IsActive && !d.User.IsDelete)
                .Select(d => new ShowUsersViewModel
                {
                    UserName = d.User.UserName,
                    Email = d.User.Email,
                    RegisterDate = d.User.RegisterDate,
                    UserId = d.UserId,
                    UserType = UserTypes.Driver.ToString()
                }).ToList();

            return driverUsers;
        }

        public User GetUser(int userId)
        {
            return _adminRepository.GetUser(userId);
        }

        public int GetUserIdByEmail(string email)
        {
            return _adminRepository.GetUserIdByEmail(email);
        }

        public List<ShowUsersViewModel> GetUsersToShow()
        {
            return _adminRepository.GetAllUsers()
                .Select(u => new ShowUsersViewModel()
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    RegisterDate = u.RegisterDate,
                    UserType = u.UserType
                }).ToList();
        }

        public bool IsPasswordCurrect(string userNameOrEmail, string password)
        {
            User user = _adminRepository.GetUser(_adminRepository.GetUserIdByEmail(userNameOrEmail));

            if (password != user.Password)
                return false;

            return true;
        }

        public bool LoginAdmin(AdminLoginViewModel admin)
        {
            if (_adminRepository.IsExistEmail(admin.Email)
                || _adminRepository.IsExistUserName(admin.Email))
            {
                User user = _adminRepository.GetUser(_adminRepository.GetUserIdByEmail(admin.Email));
                if (user != null)
                {
                    var salt = user.Salt;
                    string fixedPassword = PasswordHelper.EncodePassword(admin.Password, salt);

                    if (IsPasswordCurrect(admin.Email, fixedPassword))
                    {
                        if (user.UserRoles.Any(u => u.RoleId == 2 && u.UserId == user.UserId))
                        {
                            CookieOptions adminCookie = new CookieOptions()
                            {
                                Expires = DateTimeOffset.UtcNow.AddDays(7),
                                Path = "/",
                                HttpOnly = true,
                                Domain = "localhost",
                                Secure = true,
                                SameSite = SameSiteMode.Strict
                            };

                            string secretKey = _configuration["Encryption:EncryptSecretkey"];
                            _context.HttpContext.Response.Cookies.Append("Admin.Auth", EncryptHelper.Encrypt(user.UserName, secretKey), adminCookie);

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
                else
                {
                    return false;
                }

            }

            return false;
        }

        public void LogOutAdmin()
        {
            _context.HttpContext.Response.Cookies.Delete("Admin.Auth");
        }
    }
}

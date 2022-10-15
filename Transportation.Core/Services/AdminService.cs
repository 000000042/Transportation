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
    }
}

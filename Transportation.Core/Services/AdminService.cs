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

        public List<ShowContractorsViewModel> GetContractorsToShow()
        {
            List<Contractor> contractors = _adminRepository.GetContractorsForShow();
            List<ShowContractorsViewModel> contractorUsers = contractors
                .Where(c => c.User.IsActive == true)
                .Select(c => new ShowContractorsViewModel
                {
                    UserName = c.User.UserName,
                    RegisterDate = c.User.RegisterDate,
                    UserId = c.UserId,
                    FacePicture = c.User.FacePicture,
                    IdentificationCard = c.User.IdentificationCard,
                    NationalCode = c.User.NationalCode,
                    PhoneNumber = c.User.PhoneNumber
                }).ToList();

            return contractorUsers;
        }

        public Contractor GetContractor(int userId)
        {
            return _adminRepository.GetContractor(userId);
        }

        public Driver GetDriver(int userId)
        {
            return _adminRepository.GetDriver(userId);
        }

        public List<ShowDriversViewModel> GetDriversToShow()
        {
            List<Driver> drivers = _adminRepository.GetDriversForShow();
            List<ShowDriversViewModel> driverUsers = drivers
                .Where(d => d.User.IsActive == true)
                .Select(d => new ShowDriversViewModel
                {
                    UserName = d.User.UserName,
                    RegisterDate = d.User.RegisterDate,
                    UserId = d.UserId,
                    FacePicture = d.User.FacePicture,
                    IdentificationCard = d.User.IdentificationCard,
                    NationalCode = d.User.NationalCode,
                    PhoneNumber = d.User.PhoneNumber,
                    SmartDriverCard = d.SmartDriverCard
                }).ToList();

            return driverUsers;
        }

        public List<ShowContractorsViewModel> GetPendingContractors()
        {
            List<Contractor> contractors = _adminRepository.GetContractorsForShow();
            List<ShowContractorsViewModel> contractorUsers = contractors
                .Where(c => c.User.IsActive == false)
                .Select(c => new ShowContractorsViewModel
                {
                    UserName = c.User.UserName,
                    RegisterDate = c.User.RegisterDate,
                    UserId = c.UserId,
                    FacePicture = c.User.FacePicture,
                    IdentificationCard = c.User.IdentificationCard,
                    NationalCode = c.User.NationalCode,
                    PhoneNumber = c.User.PhoneNumber
                }).ToList();

            return contractorUsers;
        }

        public List<ShowDriversViewModel> GetPendingDrivers()
        {
            List<Driver> drivers = _adminRepository.GetDriversForShow();
            List<ShowDriversViewModel> driverUsers = drivers
                .Where(d => !d.User.IsActive && !d.User.IsDelete)
                .Select(d => new ShowDriversViewModel
                {
                    UserName = d.User.UserName,
                    RegisterDate = d.User.RegisterDate,
                    UserId = d.UserId,
                    FacePicture = d.User.FacePicture,
                    IdentificationCard = d.User.IdentificationCard,
                    NationalCode = d.User.NationalCode,
                    PhoneNumber = d.User.PhoneNumber,
                    SmartDriverCard = d.SmartDriverCard
                }).ToList();

            return driverUsers;
        }

        public User GetUser(int userId)
        {
            return _adminRepository.GetUser(userId);
        }
    }
}

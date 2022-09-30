using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.Core.Repositories;
using Transportation.Core.Security;
using Transportation.DataLayer.Entities.Contract;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Services
{
    public class ContractService : IContractService
    {
        private IContractRepository _contractRepository;
        private IAdminService _adminService;
        private IAccountRepository _accountRepository;
        private HttpContextAccessor _context;
        private IConfiguration _configuration;

        public ContractService(IContractRepository contractRepository,
            IAdminService adminService, IConfiguration configuration,
            IAccountRepository accountRepository)
        {
            _contractRepository = contractRepository;
            _adminService = adminService;
            _context = new HttpContextAccessor();
            _configuration = configuration;
            _accountRepository = accountRepository;
        }

        public int AddCargoAnnounce(CargoAnnounceViewModel announce)
        {
            if (announce == null)
                return 0;

            CargoAnnounce newAnnounce = new CargoAnnounce()
            {
                CargoType = announce.CargoType,
                CargoWeight = announce.CargoWeight,
                Description = announce.Description,
                Beginning = announce.Beginning,
                Destination = announce.Destination,
                DriverFee = announce.DriverFee,
                PackageType = announce.PackageType,
                ContractorId = announce.ContractorId
            };
            _contractRepository.CreateAnnounce(newAnnounce);

            foreach (int typeId in announce.TruckTypes)
            {
                CargoTruckType newType = new CargoTruckType()
                {
                    TypeId = typeId,
                    AnnounceId = newAnnounce.AnnounceId
                };

                _contractRepository.AddTruckTypesToAnnounce(newType);
            }

            return newAnnounce.AnnounceId;
        }

        public CargoAnnounce GetCargoAnnounceById(int announceId)
        {
            return _contractRepository.GetCargoAnnounceById(announceId);
        }

        public List<SelectListItem> GetContractorsAsItems()
        {
            List<UsersToSelectList> users = _contractRepository.GetAllContractorsToSelect();

            return users.Select(s => new SelectListItem()
            {
                Text = s.FullName,
                Value = s.UserId.ToString()
            }).ToList();
        }

        public List<ShowContractsViewModel> GetContractsForShow()
        {
            List<ShowContractsViewModel> contracts = _contractRepository.GetContracts()
                .Select(c => new ShowContractsViewModel()
            {
                    ContractId = c.ContractId,
                    CargoType = c.Announce.CargoType,
                    Beginning = c.Announce.Beginning,
                    Destination = c.Announce.Destination,
                    SignDate = c.SignDate
            }).ToList();

            return contracts;
        }

        public ContractSign GetContractToShow(int contractId)
        {
            return _contractRepository.GetContract(contractId);
        }

        public List<SelectListItem> GetDriversAsItems()
        {
            List<UsersToSelectList> users = _contractRepository.GetAllDriversToSelect();

            return users.Select(s => new SelectListItem()
            {
                Text = s.FullName,
                Value = s.UserId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetTruckTypesToSelect()
        {
            List<TruckTypesToSelect> types = _contractRepository.GetTruckTypesToSelect();

            return types.Select(t => new SelectListItem()
            {
                Text = t.TypeTitle,
                Value = t.TypeId.ToString()
            }).ToList();
        }

        public bool IsUserContractor(string email)
        {
            User user = _accountRepository.GetUser(_accountRepository.GetUserIdByUsernameOrEmail(email));

            if (user != null)
            {
                return user.UserRoles.Any(r => r.UserId == user.UserId && r.RoleId == 4);
            }

            return false;
        }

        public bool IsUserDriver(string email)
        {
            User user = _accountRepository.GetUser(_accountRepository.GetUserIdByUsernameOrEmail(email));

            if (user != null)
            {
                return user.UserRoles.Any(r => r.UserId == user.UserId && r.RoleId == 3);
            }

            return false;
        }

        public int SignContract(SignContractViewModel contract)
        {
            string secretKey = _configuration["Encryption:EncryptSecretKey"];
            string adminUserName = EncryptHelper.Decrypt(_context.HttpContext.Request.Cookies
                .SingleOrDefault(c => c.Key == "Admin.Auth").Value, secretKey);
            ContractSign sign = new ContractSign()
            {
                AdminId = _adminService.GetUserIdByEmail(adminUserName),
                RecievedRemittance = contract.RecievedRemittance,
                SignDate = DateTime.Now,
                IsDelete = false
            };

            return _contractRepository.SignContract(sign);
        }
    }
}

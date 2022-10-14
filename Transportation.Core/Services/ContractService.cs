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
                ContractorId = announce.ContractorId,
                CreateDate = DateTime.Now
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

        public void AddRequestToAnnounce(RequestViewModel request)
        {
            CargoRequest newRequest = new CargoRequest()
            {
                AnnounceId = request.AnnounceId,
                DriverId = request.DriverId,
                OfferPrice = request.OfferPrice,
                Description = request.Description,
                CreateDate = DateTime.Now
            };
            _contractRepository.AddRequestToAnnounce(newRequest);
        }

        public bool DeclineRequest(int requestId)
        {
            CargoRequest request = _contractRepository.GetCargoRequestById(requestId);
            if (request == null)
                return false;

            bool decline = _contractRepository.DeleteRequest(request);
            if (decline)
                return true;
            else
                return false;
        }

        public List<AnnounceViewModel> GetAnnouncesToShow()
        {
            return _contractRepository.GetAnnounces()
                .Where(a => !a.Contracts.Any(c => c.AnnounceId == a.AnnounceId))
                .Select(a => new AnnounceViewModel()
                {
                    AnnounceId = a.AnnounceId,
                    CreateDate = a.CreateDate,
                    CargoType = a.CargoType,
                    CargoWeight = a.CargoWeight,
                    Beginning = a.Beginning,
                    Destination = a.Destination
                }).ToList();
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
                    SignDate = c.SignDate,
                    DriverFee = int.Parse(c.Announce.DriverFee),
                    UserName = c.Admin.User.UserName,
                    RecievedRemittance = c.RecievedRemittance,
                    TruckFleetCode = c.Request.Driver.TruckFleetCode,
                    DeleteDate = c.DeleteSignDate
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

        public List<CargoRequest> GetRequestsForAnnounce(int announceId)
        {
            return _contractRepository.GetRequestsForAnnounce(announceId);
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

        public bool IsExistAnnounce(int announceId)
        {
            return _contractRepository.IsExistAnnounce(announceId);
        }

        public bool IsExistContractAnnounce(int announceId)
        {
            return _contractRepository.IsExistContractAnnounce(announceId);
        }

        public bool IsExistRequests(int announceId)
        {
            return _contractRepository.IsExistRequests(announceId);
        }

        public bool IsUserContractor(string email)
        {
            User user = _accountRepository.GetUser(_accountRepository.GetAdminIdByUserName(email));

            if (user != null)
            {
                return user.UserRoles.Any(r => r.UserId == user.UserId && r.RoleId == 4);
            }

            return false;
        }

        public bool IsUserDriver(string email)
        {
            User user = _accountRepository.GetUser(_accountRepository.GetUserIdByUserName(email));

            if (user != null)
            {
                return user.UserRoles.Any(r => r.UserId == user.UserId && r.RoleId == 3);
            }

            return false;
        }

        public int SignContract(int requestId)
        {
            CargoRequest request = _contractRepository.GetCargoRequestById(requestId);

            if (request == null)
                return 0;

            int adminId = _accountRepository.GetAdminIdByUserName(_context.HttpContext.User.Identity.Name);
            ContractSign sign = new ContractSign()
            {
                AdminId = adminId,
                AnnounceId = request.AnnounceId,
                RequestId = request.RequestId,
                RecievedRemittance = 500000,
                SignDate = DateTime.Now,
                IsDelete = false,
                DeleteSignDate = DateTime.Now.AddDays(1),
            };

            return _contractRepository.SignContract(sign);
        }
    }
}

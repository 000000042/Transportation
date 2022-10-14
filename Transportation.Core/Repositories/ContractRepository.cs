using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.DataLayer.Context;
using Transportation.DataLayer.Entities.Contract;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private TransportMainContext _context;

        public ContractRepository(TransportMainContext context)
        {
            _context = context;
        }

        public void AddRequestToAnnounce(CargoRequest request)
        {
            _context.CargoRequests.Add(request);
            _context.SaveChanges();
        }

        public void AddTruckTypesToAnnounce(CargoTruckType type)
        {
            _context.CargoTruckTypes.Add(type);
            _context.SaveChanges();
        }

        public int CreateAnnounce(CargoAnnounce announce)
        {
            _context.CargoAnnounces.Add(announce);
            _context.SaveChanges();

            return announce.AnnounceId;
        }

        public bool DeleteRequest(CargoRequest request)
        {
            request.IsDelete = true;
            _context.CargoRequests.Update(request);
            _context.SaveChanges();

            return true;
        }

        public List<UsersToSelectList> GetAllContractorsToSelect()
        {
            List<UsersToSelectList> result = _context.UserRoles
                .Include(u => u.User)
                .ThenInclude(c => c.Contractor)
                .Where(r => r.RoleId == 4)
                .Select(u => new UsersToSelectList()
                {
                    FullName = u.User.FirstName + " " + u.User.LastName,
                    UserId = u.UserId
                }).ToList();

            return result;
        }

        public List<UsersToSelectList> GetAllDriversToSelect()
        {
            List<UsersToSelectList> result = _context.UserRoles
                .Include(u => u.User)
                .ThenInclude(d => d.Driver)
                .Where(r => r.RoleId == 3)
                .Select(u => new UsersToSelectList()
                {
                    FullName = u.User.FirstName + " " + u.User.LastName,
                    UserId = u.UserId
                }).ToList();

            return result;
        }

        public List<CargoAnnounce> GetAnnounces()
        {
            return _context.CargoAnnounces
                .Include(c => c.Contracts)
                .ToList();
        }

        public CargoAnnounce GetCargoAnnounceById(int announceId)
        {
            return _context.CargoAnnounces.Include(r => r.Requests)
                .Include(t => t.CargoTruckTypes)
                .SingleOrDefault(a => a.AnnounceId == announceId);
        }

        public CargoRequest GetCargoRequestById(int requestId)
        {
            return _context.CargoRequests
                .Include(a => a.Announce)
                .ThenInclude(c => c.Contractor)
                .ThenInclude(u => u.User)
                .Include(d => d.Driver)
                .ThenInclude(u => u.User)
                .SingleOrDefault(r => r.RequestId == requestId);
        }

        public ContractSign GetContract(int contractId)
        {
            ContractSign contract = _context.ContractSigns.Include(a => a.Announce).ThenInclude(d => d.Contractor)
                .Include(r => r.Request).ThenInclude(d => d.Driver)
                .SingleOrDefault(co => co.ContractId == contractId);
            return contract;
        }

        public List<ContractSign> GetContracts()
        {
            return _context.ContractSigns.Include(a => a.Announce)
                .ThenInclude(c => c.Contractor)
                .Include(r => r.Request)
                .ThenInclude(d => d.Driver)
                .Include(a => a.Admin)
                .ThenInclude(u => u.User)
                .ToList();
        }

        public List<CargoRequest> GetRequestsForAnnounce(int announceId)
        {
            return _context.CargoRequests
                .Include(a => a.Announce)
                .ThenInclude(c => c.Contractor)
                .ThenInclude(u => u.User)
                .Include(d => d.Driver)
                .ThenInclude(u => u.User)
                .Where(a => a.AnnounceId == announceId).ToList();
        }

        public List<TruckTypesToSelect> GetTruckTypesToSelect()
        {
            List<TruckTypesToSelect> result = _context.TruckTypes
    .Select(t => new TruckTypesToSelect()
    {
        TypeId = t.TypeId,
        TypeTitle = t.TypeTitle
    }).ToList();

            return result;
        }

        public bool IsExistAnnounce(int announceId)
        {
            return _context.CargoAnnounces.Any(a => a.AnnounceId == announceId);
        }

        public bool IsExistContractAnnounce(int announceId)
        {
            return _context.ContractSigns.Any(c => c.AnnounceId == announceId);
        }

        public bool IsExistRequests(int announceId)
        {
            return _context.CargoRequests.Any(r => r.AnnounceId == announceId);
        }

        public int SignContract(ContractSign contract)
        {
            _context.ContractSigns.Add(contract);
            _context.SaveChanges();

            return contract.ContractId;
        }
    }
}

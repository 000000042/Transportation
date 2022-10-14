using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.DataLayer.Entities.Contract;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Repositories
{
    public interface IContractRepository
    {
        List<ContractSign> GetContracts();

        List<UsersToSelectList> GetAllContractorsToSelect();

        List<UsersToSelectList> GetAllDriversToSelect();

        int SignContract(ContractSign contract);

        ContractSign GetContract(int contractId);

        List<TruckTypesToSelect> GetTruckTypesToSelect();

        int CreateAnnounce(CargoAnnounce announce);

        void AddTruckTypesToAnnounce(CargoTruckType type);

        CargoAnnounce GetCargoAnnounceById(int announceId);

        List<CargoAnnounce> GetAnnounces();

        void AddRequestToAnnounce(CargoRequest request);

        List<CargoRequest> GetRequestsForAnnounce(int announceId);

        CargoRequest GetCargoRequestById(int requestId);

        bool DeleteRequest(CargoRequest request);

        bool IsExistContractAnnounce(int announceId);

        bool IsExistRequests(int announceId);

        bool IsExistAnnounce(int announceId);
    }
}

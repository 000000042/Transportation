using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AccountDTO;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Repositories
{
    public interface IAccountRepository
    {
        bool IsExistEmail(string email);

        bool IsExistPhoneNumber(string phoneNumber);

        bool IsExistUserName(string userName);

        int AddDriver(Driver register);

        int AddContractor(Contractor register);

        int AddUser(User register);

        User GetUser(int userId);

        int GetUserIdByUsername(string username);

        bool CheckUserPermissions(int permissionId, string userName);

        int GetUserIdByEmail(string email);

        void AddRoleToUser(UserRole newRole);

        int GetDriverIdByUserName(string userName);

        int GetContractorIdByUserName(string userName);

        void AddTruckTypesToDriver(DriverTruck truck);

        int GetUserIdByPhoneNumber(string phoneNumber);
    }
}

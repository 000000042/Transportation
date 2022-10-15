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

        bool CheckUserPermissions(int permissionId, string userName);

        void AddRoleToUser(UserRole newRole);

        int GetDriverIdByUserName(string userName);

        int GetContractorIdByUserName(string userName);

        void AddTruckTypesToDriver(DriverTruck truck);

        int GetAdminIdByUserName(string userName);

        int GetUserIdByPhoneNumber(string phoneNumber);

        int GetUserIdByUserName(string userName);

        bool CheckUserRoles(int roleId, string userName);

        List<int> GetUserRoles(int userId);
    }
}

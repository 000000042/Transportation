using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Repositories
{
    public interface IAdminRepository
    {
        #region User

        List<Driver> GetDriversForShow();

        List<Contractor> GetContractorsForShow();

        int UpdateUser(User user);

        Driver GetDriver(int userId);

        Contractor GetContractor(int userId);

        User GetUser(int userId);

        bool IsRoleExist(int roleId, int userId);

        void GiveRoleToUser(UserRole userRole);

        bool IsPasswordExist(string password);

        int GetUserIdByEmail(string email);

        bool IsExistPhoneNumber(string phoneNumber);

        bool IsExistUserName(string userName);

        bool IsExistEmail(string email);

        bool CheckUserPermissions(int permissionId, string email);

        List<User> GetAllUsers();

        #endregion
    }
}

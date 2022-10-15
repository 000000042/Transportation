using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AccountDTO;

namespace Transportation.Core.Services
{
    public interface IAccountService
    {
        int RegisterDriver(DriverRegisterViewModel register);

        int RegisterContractor(ContractorRegisterViewModel register);

        bool LoginUser(UserLoginViewModel login);

        bool IsPasswordCurrect(string userNameOrEmail, string password);

        void LogOutUser();

        bool CheckUserPermissions(int permissionId, string userName);

        int GetDriverIdByUserName(string userName);

        int GetContractorIdByUserName(string userName);

        int GetAdminIdByUserName(string userName);

        bool CheckUserRoles(int roleId, string userName);
    }
}

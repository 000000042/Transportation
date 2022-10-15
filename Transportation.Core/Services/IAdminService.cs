 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Services
{
    public interface IAdminService
    {
        #region User

        List<ShowUsersViewModel> GetDriversToShow();

        List<ShowUsersViewModel> GetContractorsToShow();

        List<ShowUsersViewModel> GetPendingDrivers();

        List<ShowUsersViewModel> GetPendingContractors();

        bool ActiveUser(int userId);

        Driver GetDriver(int userId);

        Contractor GetContractor(int userId);

        bool DeleteUser(int userId);

        User GetUser(int userId);

        List<ShowUsersViewModel> GetUsersToShow();

        #endregion
    }
}

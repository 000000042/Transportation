using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AccountDTO;
using Transportation.DataLayer.Context;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private TransportMainContext _context;

        public AccountRepository(TransportMainContext context)
        {
            _context = context;
        }

        public User GetUser(int userId)
        {
            IQueryable<User> user = _context.Users.Include(r => r.UserRoles).Where(u => u.UserId == userId);
            if (user.Any(u => u.UserRoles.Any(r => r.RoleId == 3)))
                user.Include(d => d.Driver);

            if (user.Any(u => u.UserRoles.Any(r => r.RoleId == 4)))
                user.Include(d => d.Contractor);

            return user.SingleOrDefault();
        }

        public void GiveDriverRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }

        public bool IsExistEmail(string email)
        {
            bool isEmailExist = _context.Users.Any(u => u.Email == email);

            if (isEmailExist)
                return true;

            return false;
        }

        public bool IsExistPhoneNumber(string phoneNumber)
        {
            bool isNumberExist = _context.Users.Any(u => u.PhoneNumber == phoneNumber);

            if (isNumberExist)
                return true;

            return false;
        }

        public bool IsExistUserName(string userName)
        {
            bool isUserNameExist = _context.Users.Any(u => u.UserName == userName);

            if (isUserNameExist)
                return true;

            return false;
        }

        public int AddContractor(Contractor register)
        {
            _context.Contractors.Add(register);
            _context.SaveChanges();

            return register.UserId;
        }

        public int AddDriver(Driver register)
        {
            _context.Drivers.Add(register);
            _context.SaveChanges();

            return register.UserId;
        }

        public int AddUser(User register)
        {
            _context.Users.Add(register);
            _context.SaveChanges();

            return register.UserId;
        }

        public bool CheckUserPermissions(int permissionId, string userName)
        {
            int userId = GetUserIdByUserName(userName);

            List<int> UserRoles = _context.UserRoles.Where(u => u.UserId == userId)
                .Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolePermissions = _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(r => r.RoleId).ToList();

            return RolePermissions.Any(p => UserRoles.Contains(p));
        }

        public void AddRoleToUser(UserRole newRole)
        {
            _context.UserRoles.Add(newRole);
            _context.SaveChanges();
        }

        public int GetDriverIdByUserName(string userName)
        {
            var user = _context.Users
                .Include(a => a.Driver)
                .SingleOrDefault(u => u.UserName == userName);

            return user.Driver.DriverId;
        }

        public int GetContractorIdByUserName(string userName)
        {
            var user = _context.Users
                .Include(a => a.Contractor)
                .SingleOrDefault(u => u.UserName == userName);

            return user.Contractor.ContractorId;
        }

        public void AddTruckTypesToDriver(DriverTruck truck)
        {
            _context.DriverTrucks.Add(truck);
            _context.SaveChanges();
        }
        public int GetAdminIdByUserName(string userName)
        {
            var user = _context.Users
                .Include(a => a.Admin)
                .SingleOrDefault(u => u.UserName == userName);
            return user.Admin.AdminId;
        }

        public int GetUserIdByPhoneNumber(string phoneNumber)
        {
            return _context.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber).UserId;
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName).UserId;
        }

        public bool CheckUserRoles(int roleId, string userName)
        {
            int userId = GetUserIdByUserName(userName);

            List<int> UserRoles = GetUserRoles(userId);

            return UserRoles.Any(r => r == roleId);
        }

        public List<int> GetUserRoles(int userId)
        {
            return _context.UserRoles.Where(u => u.UserId == userId)
                .Select(r => r.RoleId).ToList();
        }
    }
}

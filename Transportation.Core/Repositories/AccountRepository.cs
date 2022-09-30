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

        public int GetUserIdByUsernameOrEmail(string usernameOrEmail)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == usernameOrEmail || u.Email == usernameOrEmail).UserId;
        }

        public void GiveDriverRole(UserRoles userRole)
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
            bool isNumberExistInDrivers = _context.Users.Any(u => u.Driver.PhoneNumber == phoneNumber);
            bool isNumberExistInCompanies = _context.Users.Any(u => u.Contractor.PhoneNumber == phoneNumber);

            if (isNumberExistInDrivers || isNumberExistInCompanies)
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
            int userId = _context.Users.SingleOrDefault(u => u.UserName == userName).UserId;

            List<int> UserRoles = _context.UserRoles.Where(u => u.UserId == userId)
                .Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolePermissions = _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(r => r.RoleId).ToList();

            return RolePermissions.Any(p => UserRoles.Contains(p));
        }

        public int GetUserIdByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email).UserId;
        }

        public void AddRoleToUser(UserRoles newRole)
        {
            _context.UserRoles.Add(newRole);
            _context.SaveChanges();
        }

        public int GetDriverIdByUserName(string userName)
        {
            return _context.Drivers.Include(u => u.User)
                .SingleOrDefault(u => u.User.UserName == userName).DriverId;
        }

        public int GetContractorIdByUserName(string userName)
        {
            return _context.Contractors.Include(u => u.User)
                .SingleOrDefault(u => u.User.UserName == userName).ContractorId;
        }
    }
}

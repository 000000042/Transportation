using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Context;
using Transportation.DataLayer.Entities.User;

namespace Transportation.Core.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private TransportMainContext _context;

        public AdminRepository(TransportMainContext context)
        {
            _context = context;
        }

        public List<Contractor> GetContractorsForShow()
        {
            return _context.Contractors.Include(u => u.User).ToList();
        }

        public Driver GetDriver(int userId)
        {
            return _context.Drivers.Include(u => u.User).SingleOrDefault(d => d.UserId == userId);
        }

        public List<Driver> GetDriversForShow()
        {
            return _context.Drivers.Include(u => u.User).ToList();
        }

        public Contractor GetContractor(int userId)
        {
            return _context.Contractors.Include(u => u.User).SingleOrDefault(d => d.UserId == userId);
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

        public bool IsRoleExist(int roleId, int userId)
        {
            return _context.UserRoles.Any(d => d.RoleId == roleId && d.UserId == userId);
        }

        public void GiveRoleToUser(UserRoles userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }

        public bool IsPasswordExist(string password)
        {
            return _context.Users.Any(u => u.Password == password);
        }

        public int GetUserIdByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email).UserId;
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

        public bool IsExistEmail(string email)
        {
            bool isEmailExist = _context.Users.Any(u => u.Email == email);

            if (isEmailExist)
                return true;

            return false;
        }

        public bool CheckUserPermissions(int permissionId, string email)
        {
            int userId = _context.Users.SingleOrDefault(u => u.Email == email).UserId;

            List<int> UserRoles = _context.UserRoles.Where(u => u.UserId == userId)
                .Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolePermissions = _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(r => r.RoleId).ToList();

            return RolePermissions.Any(p => UserRoles.Contains(p));
        }

        public int UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            return user.UserId;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}

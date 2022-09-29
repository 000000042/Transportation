using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.DataLayer.Entities.Contract;
using Transportation.DataLayer.Entities.Permission;
using Transportation.DataLayer.Entities.User;

namespace Transportation.DataLayer.Context
{
    public class TransportMainContext : DbContext
    {
        public TransportMainContext(DbContextOptions options) : base(options)
        {

        }

        #region User

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Contract & Cargos

        public DbSet<ContractSign> ContractSigns { get; set; }

        public DbSet<CargoAnnounce> CargoAnnounces { get; set; }

        public DbSet<TruckType> TruckTypes { get; set; }

        public DbSet<CargoTruckType> CargoTruckTypes { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
   .SelectMany(t => t.GetForeignKeys())
   .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);

            base.OnModelCreating(modelBuilder);
        }
    }
}

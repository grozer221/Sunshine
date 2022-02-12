using Microsoft.EntityFrameworkCore;
using Sunshine.Enums;
using Sunshine.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sunshine
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<New> News { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue(Role.user.ToString());
        //    base.OnModelCreating(modelBuilder);
        //}

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                DateTime dateTimeNow = DateTime.Now; 
                if (entity.State == EntityState.Added)
                    ((BaseModel)entity.Entity).CreatedAt = dateTimeNow;
                ((BaseModel)entity.Entity).UpdatedAt = dateTimeNow;
            }
        }
    }
}

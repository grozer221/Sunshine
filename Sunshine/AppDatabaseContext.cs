using Microsoft.EntityFrameworkCore;
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
        public DbSet<File> Files { get; set; }

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

        public static string GetConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable("JAWSDB_URL");
            connectionString = connectionString.Split("//")[1];
            string user = connectionString.Split(':')[0];
            connectionString = connectionString.Replace(user, "").Substring(1);
            string password = connectionString.Split('@')[0];
            if (!string.IsNullOrEmpty(password))
                connectionString = connectionString.Replace(password, "");
            connectionString = connectionString.Substring(1);
            string server = connectionString.Split(':')[0];
            connectionString = connectionString.Replace(server, "").Substring(1);
            string port = connectionString.Split('/')[0];
            string database = connectionString.Split('/')[1];
            connectionString = $"server={server};database={database};user={user};password={password};port={port};";
            return connectionString;
        }
    }
}

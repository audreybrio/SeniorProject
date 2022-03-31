using Microsoft.EntityFrameworkCore;
using StudentMultiTool.Backend.Services.Users;

namespace StudentMultiTool.Backend.Models.AccessModel
{
    public class RBACContext: DbContext
    {

        public RBACContext(DbContextOptions<RBACContext> options) : base(options)
        {
            
        }

        public DbSet<UserAccount> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasOne(e => e.Roles)
                .WithMany(e => e.Permissions);
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Roles);
        }
    }
}

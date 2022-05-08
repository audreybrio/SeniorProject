using Microsoft.EntityFrameworkCore;
using StudentMultiTool.Backend.Services.Authorization.Entities;

namespace StudentMultiTool.Backend.Services.Authorization.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            Options.UseInMemoryDatabase("Marvel");
        }
    }
}

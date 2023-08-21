using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Context
{
    public class applicationDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-665C9QB\\SQLEXPRESS;Initial Catalog=projectMVC;Integrated Security=true;TrustServerCertificate=true");
        }
        public DbSet<Device> devices { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }

    }
}

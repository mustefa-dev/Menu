using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }


        public DbSet<User> User => Set<User>();
        public DbSet<Menu.Models.Item> Items => Set<Menu.Models.Item>();
        public DbSet<Order> Orders => Set<Order>();
    }
}
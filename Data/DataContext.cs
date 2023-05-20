using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }


        public DbSet<User> User => Set<User>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Order> Orders => Set<Order>();
    }
}
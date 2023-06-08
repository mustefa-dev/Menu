using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.Data{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }


        public DbSet<User> User => Set<User>();
        public DbSet<Models.Item> Items => Set<Menu.Models.Item>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Drink> Drinks => Set<Drink>();
    }
}
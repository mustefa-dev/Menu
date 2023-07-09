    using Menu.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Menu.Data{
        public class DataContext : DbContext{
            public DataContext(DbContextOptions<DataContext> options) : base(options) {
            }


            public DbSet<User> User => Set<User>();
            public DbSet<Category> Categories { get; set; }
            public DbSet<Section> Sections { get; set; }
            public DbSet<Item> Items { get; set; }

        }
    }
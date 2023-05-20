using Auth.Models;

namespace Auth.Data{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }


        public DbSet<User> User => Set<User>();
    }
}
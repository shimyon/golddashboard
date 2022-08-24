using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Data.Common;
using AngelOneAdmin.Models;

namespace services
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        public ApplicationDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<NiftyIndex> NiftyINdex { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<tradingindex> TradingIndex { get; set; }
      
    }
}

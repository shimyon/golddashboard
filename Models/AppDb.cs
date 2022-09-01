using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngelOneAdmin.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDb: DbContext
    {
        public AppDb(): base("name=DefaultConnection")
        {

        }
        
        public DbSet<NiftyIndex> NiftyINdex { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<tradingindex> TradingIndex { get; set; }
        public DbSet<ManualTrading> ManualTrading { get; set; }
        public DbSet<BankNiftyIndex> BankNiftyIndex { get; set; }
        public DbSet<TradeBook> TradeBook { get; set; }
        public DbSet<Setting> Settings { get; set; }

    }
}
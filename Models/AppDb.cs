using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace GoldDashboard.Models
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDb: IdentityDbContext<AppUsers>
    {
        public AppDb(): base("name=DefaultConnection")
        {

        }

        public static AppDb Create()
        {
            return new AppDb();
        }
        public DbSet<Currency> setting { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Ignore<User>();
        //    modelBuilder.Ignore<NiftyIndex>();
        //    modelBuilder.Ignore<tradingindex>();
        //    modelBuilder.Ignore<ManualTrading>();
        //    modelBuilder.Ignore<BankNiftyIndex>();
        //    modelBuilder.Ignore<TradeBook>();
        //    modelBuilder.Ignore<Setting>();
        //    modelBuilder.Ignore<Holiday>();
        //}


    }
}
namespace GoldDashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dailyprofitfieldadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DailyProfit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "DailyPercent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DailyPercent");
            DropColumn("dbo.AspNetUsers", "DailyProfit");
        }
    }
}

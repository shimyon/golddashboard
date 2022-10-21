namespace GoldDashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Percent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AspNetUsers", "DailyPercent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DailyPercent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AspNetUsers", "Percent");
        }
    }
}

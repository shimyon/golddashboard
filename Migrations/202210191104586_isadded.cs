namespace GoldDashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsReferralAdded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsReferralAdded");
        }
    }
}

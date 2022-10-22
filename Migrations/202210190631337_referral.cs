namespace GoldDashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class referral : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ReferralPersonEmail", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "ReferralPersonName", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ReferralPersonName");
            DropColumn("dbo.AspNetUsers", "ReferralPersonEmail");
        }
    }
}

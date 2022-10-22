namespace GoldDashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class referralchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ReferralPersonId", c => c.String(unicode: false));
            DropColumn("dbo.AspNetUsers", "ReferralPersonEmail");
            DropColumn("dbo.AspNetUsers", "ReferralPersonName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ReferralPersonName", c => c.String(unicode: false));
            AddColumn("dbo.AspNetUsers", "ReferralPersonEmail", c => c.String(unicode: false));
            DropColumn("dbo.AspNetUsers", "ReferralPersonId");
        }
    }
}

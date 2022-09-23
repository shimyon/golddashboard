namespace GoldDashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goldusername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GoldUserName", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "GoldUserName");
        }
    }
}

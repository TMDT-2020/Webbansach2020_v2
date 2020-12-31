namespace Webbansach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "StatusPayment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "StatusPayment");
        }
    }
}

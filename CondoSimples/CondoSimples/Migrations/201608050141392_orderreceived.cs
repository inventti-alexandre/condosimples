namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderreceived : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderModels", "Received", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderModels", "Received");
        }
    }
}

namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Borrow2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BorrowModels", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BorrowModels", "MyProperty", c => c.Int(nullable: false));
        }
    }
}

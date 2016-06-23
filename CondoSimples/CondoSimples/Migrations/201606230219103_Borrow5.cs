namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Borrow5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BorrowModels", "DateReturn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BorrowModels", "DateComplete", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BorrowModels", "DateComplete", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BorrowModels", "DateReturn", c => c.DateTime());
        }
    }
}

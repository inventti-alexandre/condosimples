namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Borrow4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BorrowModels", "DateReturn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BorrowModels", "DateReturn", c => c.DateTime(nullable: false));
        }
    }
}

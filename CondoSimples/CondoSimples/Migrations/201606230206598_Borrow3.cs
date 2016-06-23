namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Borrow3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BorrowModels", "UserLending_ID", "dbo.UserModels");
            DropForeignKey("dbo.BorrowModels", "UserRequest_ID", "dbo.UserModels");
            DropIndex("dbo.BorrowModels", new[] { "UserLending_ID" });
            DropIndex("dbo.BorrowModels", new[] { "UserRequest_ID" });
            AddColumn("dbo.BorrowModels", "IdUserRequest", c => c.String());
            AddColumn("dbo.BorrowModels", "IdUserLending", c => c.String());
            DropColumn("dbo.BorrowModels", "UserLending_ID");
            DropColumn("dbo.BorrowModels", "UserRequest_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BorrowModels", "UserRequest_ID", c => c.Int());
            AddColumn("dbo.BorrowModels", "UserLending_ID", c => c.Int());
            DropColumn("dbo.BorrowModels", "IdUserLending");
            DropColumn("dbo.BorrowModels", "IdUserRequest");
            CreateIndex("dbo.BorrowModels", "UserRequest_ID");
            CreateIndex("dbo.BorrowModels", "UserLending_ID");
            AddForeignKey("dbo.BorrowModels", "UserRequest_ID", "dbo.UserModels", "ID");
            AddForeignKey("dbo.BorrowModels", "UserLending_ID", "dbo.UserModels", "ID");
        }
    }
}

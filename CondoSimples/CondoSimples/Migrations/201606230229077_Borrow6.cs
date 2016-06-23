namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Borrow6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BorrowModels", "IdUserLending_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.BorrowModels", "IdUserRequest_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BorrowModels", "IdUserLending_Id");
            CreateIndex("dbo.BorrowModels", "IdUserRequest_Id");
            AddForeignKey("dbo.BorrowModels", "IdUserLending_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BorrowModels", "IdUserRequest_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.BorrowModels", "IdUserRequest");
            DropColumn("dbo.BorrowModels", "IdUserLending");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BorrowModels", "IdUserLending", c => c.String());
            AddColumn("dbo.BorrowModels", "IdUserRequest", c => c.String());
            DropForeignKey("dbo.BorrowModels", "IdUserRequest_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BorrowModels", "IdUserLending_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BorrowModels", new[] { "IdUserRequest_Id" });
            DropIndex("dbo.BorrowModels", new[] { "IdUserLending_Id" });
            DropColumn("dbo.BorrowModels", "IdUserRequest_Id");
            DropColumn("dbo.BorrowModels", "IdUserLending_Id");
        }
    }
}

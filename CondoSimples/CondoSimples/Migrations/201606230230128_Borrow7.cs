namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Borrow7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BorrowModels", name: "IdUserLending_Id", newName: "UserLending_Id");
            RenameColumn(table: "dbo.BorrowModels", name: "IdUserRequest_Id", newName: "UserRequest_Id");
            RenameIndex(table: "dbo.BorrowModels", name: "IX_IdUserLending_Id", newName: "IX_UserLending_Id");
            RenameIndex(table: "dbo.BorrowModels", name: "IX_IdUserRequest_Id", newName: "IX_UserRequest_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BorrowModels", name: "IX_UserRequest_Id", newName: "IX_IdUserRequest_Id");
            RenameIndex(table: "dbo.BorrowModels", name: "IX_UserLending_Id", newName: "IX_IdUserLending_Id");
            RenameColumn(table: "dbo.BorrowModels", name: "UserRequest_Id", newName: "IdUserRequest_Id");
            RenameColumn(table: "dbo.BorrowModels", name: "UserLending_Id", newName: "IdUserLending_Id");
        }
    }
}

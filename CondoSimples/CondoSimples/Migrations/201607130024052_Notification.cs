namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        DateRegister = c.DateTime(nullable: false),
                        Condo_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CondoModels", t => t.Condo_ID)
                .ForeignKey("dbo.UserModels", t => t.User_ID)
                .Index(t => t.Condo_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationModels", "User_ID", "dbo.UserModels");
            DropForeignKey("dbo.NotificationModels", "Condo_ID", "dbo.CondoModels");
            DropIndex("dbo.NotificationModels", new[] { "User_ID" });
            DropIndex("dbo.NotificationModels", new[] { "Condo_ID" });
            DropTable("dbo.NotificationModels");
        }
    }
}

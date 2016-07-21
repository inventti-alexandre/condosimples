namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commomplaces : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommonPlaceModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WorkingTime = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScheduleModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateSchedule = c.DateTime(nullable: false),
                        Place_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CommonPlaceModels", t => t.Place_ID)
                .Index(t => t.Place_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleModels", "Place_ID", "dbo.CommonPlaceModels");
            DropIndex("dbo.ScheduleModels", new[] { "Place_ID" });
            DropTable("dbo.ScheduleModels");
            DropTable("dbo.CommonPlaceModels");
        }
    }
}

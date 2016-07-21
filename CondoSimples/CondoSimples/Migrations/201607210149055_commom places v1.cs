namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commomplacesv1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommonPlaceModels", "WorkingTimes", c => c.String());
            AddColumn("dbo.CommonPlaceModels", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.CommonPlaceModels", "Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.CommonPlaceModels", "IncludeItens", c => c.String());
            AddColumn("dbo.CommonPlaceModels", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.CommonPlaceModels", "Terms", c => c.String());
            AddColumn("dbo.CommonPlaceModels", "Condo_ID", c => c.Int());
            AddColumn("dbo.ScheduleModels", "User_ID", c => c.Int());
            CreateIndex("dbo.CommonPlaceModels", "Condo_ID");
            CreateIndex("dbo.ScheduleModels", "User_ID");
            AddForeignKey("dbo.CommonPlaceModels", "Condo_ID", "dbo.CondoModels", "ID");
            AddForeignKey("dbo.ScheduleModels", "User_ID", "dbo.UserModels", "ID");
            DropColumn("dbo.CommonPlaceModels", "WorkingTime");
            DropColumn("dbo.CommonPlaceModels", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommonPlaceModels", "Description", c => c.String());
            AddColumn("dbo.CommonPlaceModels", "WorkingTime", c => c.String());
            DropForeignKey("dbo.ScheduleModels", "User_ID", "dbo.UserModels");
            DropForeignKey("dbo.CommonPlaceModels", "Condo_ID", "dbo.CondoModels");
            DropIndex("dbo.ScheduleModels", new[] { "User_ID" });
            DropIndex("dbo.CommonPlaceModels", new[] { "Condo_ID" });
            DropColumn("dbo.ScheduleModels", "User_ID");
            DropColumn("dbo.CommonPlaceModels", "Condo_ID");
            DropColumn("dbo.CommonPlaceModels", "Terms");
            DropColumn("dbo.CommonPlaceModels", "Active");
            DropColumn("dbo.CommonPlaceModels", "IncludeItens");
            DropColumn("dbo.CommonPlaceModels", "Capacity");
            DropColumn("dbo.CommonPlaceModels", "Price");
            DropColumn("dbo.CommonPlaceModels", "WorkingTimes");
        }
    }
}

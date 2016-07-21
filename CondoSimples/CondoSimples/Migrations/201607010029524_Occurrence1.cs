namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Occurrence1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OccurrenceModels", "MyProperty", c => c.Int(nullable: false));
            AddColumn("dbo.OccurrenceModels", "Solved", c => c.Boolean(nullable: false));
            AddColumn("dbo.OccurrenceModels", "User_ID", c => c.Int());
            CreateIndex("dbo.OccurrenceModels", "User_ID");
            AddForeignKey("dbo.OccurrenceModels", "User_ID", "dbo.UserModels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OccurrenceModels", "User_ID", "dbo.UserModels");
            DropIndex("dbo.OccurrenceModels", new[] { "User_ID" });
            DropColumn("dbo.OccurrenceModels", "User_ID");
            DropColumn("dbo.OccurrenceModels", "Solved");
            DropColumn("dbo.OccurrenceModels", "MyProperty");
        }
    }
}

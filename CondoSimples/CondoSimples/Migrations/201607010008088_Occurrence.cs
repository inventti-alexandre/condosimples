namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Occurrence : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OccurrenceModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DateOccurrence = c.DateTime(nullable: false),
                        Condo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CondoModels", t => t.Condo_ID)
                .Index(t => t.Condo_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OccurrenceModels", "Condo_ID", "dbo.CondoModels");
            DropIndex("dbo.OccurrenceModels", new[] { "Condo_ID" });
            DropTable("dbo.OccurrenceModels");
        }
    }
}

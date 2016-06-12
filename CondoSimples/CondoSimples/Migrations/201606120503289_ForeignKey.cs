namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AspNetUsers", "Condo_ID");
            AddForeignKey("dbo.AspNetUsers", "Condo_ID", "dbo.CondoModels", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Condo_ID", "dbo.CondoModels");
            DropIndex("dbo.AspNetUsers", new[] { "Condo_ID" });
        }
    }
}

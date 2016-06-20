namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v06 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeModels", "Condo_ID", "dbo.CondoModels");
            DropIndex("dbo.EmployeeModels", new[] { "Condo_ID" });
            DropColumn("dbo.EmployeeModels", "Condo_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeModels", "Condo_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.EmployeeModels", "Condo_ID");
            AddForeignKey("dbo.EmployeeModels", "Condo_ID", "dbo.CondoModels", "ID", cascadeDelete: true);
        }
    }
}

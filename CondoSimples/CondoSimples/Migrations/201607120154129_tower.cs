namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tower : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TowerModels", "Condo_ID", "dbo.CondoModels");
            DropIndex("dbo.TowerModels", new[] { "Condo_ID" });
            AddColumn("dbo.TowerModels", "UnitsQtd", c => c.Int(nullable: false));
            AlterColumn("dbo.TowerModels", "Condo_ID", c => c.Int());
            CreateIndex("dbo.TowerModels", "Condo_ID");
            AddForeignKey("dbo.TowerModels", "Condo_ID", "dbo.CondoModels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TowerModels", "Condo_ID", "dbo.CondoModels");
            DropIndex("dbo.TowerModels", new[] { "Condo_ID" });
            AlterColumn("dbo.TowerModels", "Condo_ID", c => c.Int(nullable: false));
            DropColumn("dbo.TowerModels", "UnitsQtd");
            CreateIndex("dbo.TowerModels", "Condo_ID");
            AddForeignKey("dbo.TowerModels", "Condo_ID", "dbo.CondoModels", "ID", cascadeDelete: true);
        }
    }
}

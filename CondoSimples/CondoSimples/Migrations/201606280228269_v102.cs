namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v102 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OutSourcerModels", "Condo_ID", c => c.Int());
            CreateIndex("dbo.OutSourcerModels", "Condo_ID");
            AddForeignKey("dbo.OutSourcerModels", "Condo_ID", "dbo.CondoModels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OutSourcerModels", "Condo_ID", "dbo.CondoModels");
            DropIndex("dbo.OutSourcerModels", new[] { "Condo_ID" });
            DropColumn("dbo.OutSourcerModels", "Condo_ID");
        }
    }
}

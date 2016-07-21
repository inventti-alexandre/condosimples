namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tentativa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CondoModels", "Address_ID", c => c.Int());
            CreateIndex("dbo.CondoModels", "Address_ID");
            AddForeignKey("dbo.CondoModels", "Address_ID", "dbo.AddressModels", "ID");
            DropColumn("dbo.CondoModels", "Address");
            DropColumn("dbo.OccurrenceModels", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OccurrenceModels", "MyProperty", c => c.Int(nullable: false));
            AddColumn("dbo.CondoModels", "Address", c => c.String());
            DropForeignKey("dbo.CondoModels", "Address_ID", "dbo.AddressModels");
            DropIndex("dbo.CondoModels", new[] { "Address_ID" });
            DropColumn("dbo.CondoModels", "Address_ID");
        }
    }
}

namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v101 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CEP = c.String(),
                        Street = c.String(),
                        Number = c.String(),
                        City = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OutSourcerModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Tel = c.String(),
                        CNPJ = c.String(),
                        Site = c.String(),
                        Contact = c.String(),
                        Address_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AddressModels", t => t.Address_ID)
                .Index(t => t.Address_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OutSourcerModels", "Address_ID", "dbo.AddressModels");
            DropIndex("dbo.OutSourcerModels", new[] { "Address_ID" });
            DropTable("dbo.OutSourcerModels");
            DropTable("dbo.AddressModels");
        }
    }
}

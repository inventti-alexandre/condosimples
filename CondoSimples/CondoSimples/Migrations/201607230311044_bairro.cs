namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bairro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressModels", "Neighborhood", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AddressModels", "Neighborhood");
        }
    }
}

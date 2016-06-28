namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v103 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OutSourcerModels", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.OutSourcerModels", "Tel", c => c.String(nullable: false));
            AlterColumn("dbo.OutSourcerModels", "CNPJ", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OutSourcerModels", "CNPJ", c => c.String());
            AlterColumn("dbo.OutSourcerModels", "Tel", c => c.String());
            AlterColumn("dbo.OutSourcerModels", "CompanyName", c => c.String());
        }
    }
}

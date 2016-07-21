namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModels", "Phone", c => c.String());
            AddColumn("dbo.UserModels", "EmailOthers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserModels", "EmailOthers");
            DropColumn("dbo.UserModels", "Phone");
        }
    }
}

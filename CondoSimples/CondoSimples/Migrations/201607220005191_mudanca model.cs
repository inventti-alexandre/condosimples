namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mudancamodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NotificationModels", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NotificationModels", "Message", c => c.String());
        }
    }
}

namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commomplacesv2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CommonPlaceModels", "WorkingTimes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommonPlaceModels", "WorkingTimes", c => c.String());
        }
    }
}

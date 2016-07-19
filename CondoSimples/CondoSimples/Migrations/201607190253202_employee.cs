namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeModels", "DutyDays", c => c.String());
            AlterColumn("dbo.EmployeeModels", "WorkShift", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeModels", "WorkShift", c => c.String(nullable: false));
            AlterColumn("dbo.EmployeeModels", "DutyDays", c => c.String(nullable: false));
        }
    }
}

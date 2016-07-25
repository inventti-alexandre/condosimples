namespace CondoSimples.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        Neighborhood = c.String(),
                        City = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BoardModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Post = c.String(nullable: false),
                        DatePost = c.DateTime(nullable: false),
                        DateExpires = c.DateTime(nullable: false),
                        Published = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Condo_ID = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CondoModels", t => t.Condo_ID, cascadeDelete: true)
                .Index(t => t.Condo_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CondoModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParkingSlots = c.Int(nullable: false),
                        Address_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AddressModels", t => t.Address_ID)
                .Index(t => t.Address_ID);
            
            CreateTable(
                "dbo.TowerModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UnitsQtd = c.Int(nullable: false),
                        Condo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CondoModels", t => t.Condo_ID)
                .Index(t => t.Condo_ID);
            
            CreateTable(
                "dbo.UnitModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Tower_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TowerModels", t => t.Tower_ID, cascadeDelete: true)
                .Index(t => t.Tower_ID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BorrowModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Quantity = c.Int(nullable: false),
                        DateRequire = c.DateTime(nullable: false),
                        DateReturn = c.DateTime(nullable: false),
                        DateComplete = c.DateTime(),
                        UserLending_Id = c.String(maxLength: 128),
                        UserRequest_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserLending_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserRequest_Id)
                .Index(t => t.UserLending_Id)
                .Index(t => t.UserRequest_Id);
            
            CreateTable(
                "dbo.CommonPlaceModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Capacity = c.Int(nullable: false),
                        IncludeItens = c.String(),
                        Active = c.Boolean(nullable: false),
                        Terms = c.String(),
                        Condo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CondoModels", t => t.Condo_ID)
                .Index(t => t.Condo_ID);
            
            CreateTable(
                "dbo.EmployeeModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Cel = c.String(),
                        Position = c.String(nullable: false),
                        DutyDays = c.String(),
                        WorkShift = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.NotificationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        DateRegister = c.DateTime(nullable: false),
                        Condo_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CondoModels", t => t.Condo_ID)
                .ForeignKey("dbo.UserModels", t => t.User_ID)
                .Index(t => t.Condo_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CPF = c.String(),
                        Name = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        Cel = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        EmailOthers = c.String(),
                        Residents = c.String(),
                        Pets = c.String(),
                        Cars = c.String(),
                        Visitors = c.String(),
                        Unit_ID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UnitModels", t => t.Unit_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Unit_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.OccurrenceModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DateOccurrence = c.DateTime(nullable: false),
                        Solved = c.Boolean(nullable: false),
                        Condo_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CondoModels", t => t.Condo_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Condo_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateReceived = c.DateTime(nullable: false),
                        Description = c.String(),
                        UserEmployee_ID = c.Int(),
                        UserRecipient_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmployeeModels", t => t.UserEmployee_ID)
                .ForeignKey("dbo.UserModels", t => t.UserRecipient_ID)
                .Index(t => t.UserEmployee_ID)
                .Index(t => t.UserRecipient_ID);
            
            CreateTable(
                "dbo.OutSourcerModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        Tel = c.String(nullable: false),
                        CNPJ = c.String(nullable: false),
                        Site = c.String(),
                        Contact = c.String(),
                        Address_ID = c.Int(),
                        Condo_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AddressModels", t => t.Address_ID)
                .ForeignKey("dbo.CondoModels", t => t.Condo_ID)
                .Index(t => t.Address_ID)
                .Index(t => t.Condo_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ScheduleModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateSchedule = c.DateTime(nullable: false),
                        Place_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CommonPlaceModels", t => t.Place_ID)
                .ForeignKey("dbo.UserModels", t => t.User_ID)
                .Index(t => t.Place_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleModels", "User_ID", "dbo.UserModels");
            DropForeignKey("dbo.ScheduleModels", "Place_ID", "dbo.CommonPlaceModels");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OutSourcerModels", "Condo_ID", "dbo.CondoModels");
            DropForeignKey("dbo.OutSourcerModels", "Address_ID", "dbo.AddressModels");
            DropForeignKey("dbo.OrderModels", "UserRecipient_ID", "dbo.UserModels");
            DropForeignKey("dbo.OrderModels", "UserEmployee_ID", "dbo.EmployeeModels");
            DropForeignKey("dbo.OccurrenceModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OccurrenceModels", "Condo_ID", "dbo.CondoModels");
            DropForeignKey("dbo.NotificationModels", "User_ID", "dbo.UserModels");
            DropForeignKey("dbo.UserModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserModels", "Unit_ID", "dbo.UnitModels");
            DropForeignKey("dbo.NotificationModels", "Condo_ID", "dbo.CondoModels");
            DropForeignKey("dbo.EmployeeModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommonPlaceModels", "Condo_ID", "dbo.CondoModels");
            DropForeignKey("dbo.BorrowModels", "UserRequest_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BorrowModels", "UserLending_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BoardModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Condo_ID", "dbo.CondoModels");
            DropForeignKey("dbo.UnitModels", "Tower_ID", "dbo.TowerModels");
            DropForeignKey("dbo.TowerModels", "Condo_ID", "dbo.CondoModels");
            DropForeignKey("dbo.CondoModels", "Address_ID", "dbo.AddressModels");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ScheduleModels", new[] { "User_ID" });
            DropIndex("dbo.ScheduleModels", new[] { "Place_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OutSourcerModels", new[] { "Condo_ID" });
            DropIndex("dbo.OutSourcerModels", new[] { "Address_ID" });
            DropIndex("dbo.OrderModels", new[] { "UserRecipient_ID" });
            DropIndex("dbo.OrderModels", new[] { "UserEmployee_ID" });
            DropIndex("dbo.OccurrenceModels", new[] { "User_Id" });
            DropIndex("dbo.OccurrenceModels", new[] { "Condo_ID" });
            DropIndex("dbo.UserModels", new[] { "User_Id" });
            DropIndex("dbo.UserModels", new[] { "Unit_ID" });
            DropIndex("dbo.NotificationModels", new[] { "User_ID" });
            DropIndex("dbo.NotificationModels", new[] { "Condo_ID" });
            DropIndex("dbo.EmployeeModels", new[] { "User_Id" });
            DropIndex("dbo.CommonPlaceModels", new[] { "Condo_ID" });
            DropIndex("dbo.BorrowModels", new[] { "UserRequest_Id" });
            DropIndex("dbo.BorrowModels", new[] { "UserLending_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.UnitModels", new[] { "Tower_ID" });
            DropIndex("dbo.TowerModels", new[] { "Condo_ID" });
            DropIndex("dbo.CondoModels", new[] { "Address_ID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "Condo_ID" });
            DropIndex("dbo.BoardModels", new[] { "User_Id" });
            DropTable("dbo.ScheduleModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OutSourcerModels");
            DropTable("dbo.OrderModels");
            DropTable("dbo.OccurrenceModels");
            DropTable("dbo.UserModels");
            DropTable("dbo.NotificationModels");
            DropTable("dbo.EmployeeModels");
            DropTable("dbo.CommonPlaceModels");
            DropTable("dbo.BorrowModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.UnitModels");
            DropTable("dbo.TowerModels");
            DropTable("dbo.CondoModels");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BoardModels");
            DropTable("dbo.AddressModels");
        }
    }
}

namespace Trident.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authentication : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("Characters", "member_MemberID", "Members");
            //DropForeignKey("Members", "team_TeamID", "Teams");
            //DropIndex("Characters", new[] { "member_MemberID" });
            //DropIndex("Members", new[] { "team_TeamID" });
            CreateTable(
                "AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 255, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("Characters");
            DropTable("Members");
            DropTable("Teams");
        }
        
        public override void Down()
        {
            CreateTable(
                "Teams",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        TeamRep = c.String(maxLength: 30, storeType: "nvarchar"),
                        TeamType = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.TeamID);
            
            CreateTable(
                "Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        MemberName = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        MemberLevel = c.Int(nullable: false),
                        MemberSpecialty = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        MemberStrikes = c.Int(nullable: false),
                        team_TeamID = c.Int(),
                    })
                .PrimaryKey(t => t.MemberID);
            
            CreateTable(
                "Characters",
                c => new
                    {
                        CharacterID = c.Int(nullable: false, identity: true),
                        CharacterName = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        CharacterWeapon = c.Int(nullable: false),
                        CharacterTreasure = c.Int(nullable: false),
                        member_MemberID = c.Int(),
                    })
                .PrimaryKey(t => t.CharacterID);
            
            DropForeignKey("AspNetUserRoles", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserLogins", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserClaims", "UserId", "AspNetUsers");
            DropForeignKey("AspNetUserRoles", "RoleId", "AspNetRoles");
            DropIndex("AspNetUserLogins", new[] { "UserId" });
            DropIndex("AspNetUserClaims", new[] { "UserId" });
            DropIndex("AspNetUsers", "UserNameIndex");
            DropIndex("AspNetUserRoles", new[] { "RoleId" });
            DropIndex("AspNetUserRoles", new[] { "UserId" });
            DropIndex("AspNetRoles", "RoleNameIndex");
            DropTable("AspNetUserLogins");
            DropTable("AspNetUserClaims");
            DropTable("AspNetUsers");
            DropTable("AspNetUserRoles");
            DropTable("AspNetRoles");
            CreateIndex("Members", "team_TeamID");
            CreateIndex("Characters", "member_MemberID");
            AddForeignKey("Members", "team_TeamID", "Teams", "TeamID");
            AddForeignKey("Characters", "member_MemberID", "Members", "MemberID");
        }
    }
}

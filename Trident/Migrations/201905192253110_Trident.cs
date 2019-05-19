namespace Trident.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trident : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 30),
                        TeamRep = c.String(nullable: false, maxLength: 30),
                        TeamType = c.String(nullable: false, maxLength: 30),
                        TeamMembers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID);
            
            AddColumn("dbo.Characters", "CharacterTreasure", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "MemberSpecialty", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Members", "MemberStrikes", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "team_TeamID", c => c.Int());
            CreateIndex("dbo.Members", "team_TeamID");
            AddForeignKey("dbo.Members", "team_TeamID", "dbo.Teams", "TeamID");
            DropColumn("dbo.Characters", "CharacterRole");
            DropColumn("dbo.Characters", "CharacterType");
            DropColumn("dbo.Members", "MemberRole");
            DropColumn("dbo.Members", "HasPic");
            DropColumn("dbo.Members", "ImgType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "ImgType", c => c.String());
            AddColumn("dbo.Members", "HasPic", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "MemberRole", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Characters", "CharacterType", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Characters", "CharacterRole", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.Members", "team_TeamID", "dbo.Teams");
            DropIndex("dbo.Members", new[] { "team_TeamID" });
            DropColumn("dbo.Members", "team_TeamID");
            DropColumn("dbo.Members", "MemberStrikes");
            DropColumn("dbo.Members", "MemberSpecialty");
            DropColumn("dbo.Characters", "CharacterTreasure");
            DropTable("dbo.Teams");
        }
    }
}

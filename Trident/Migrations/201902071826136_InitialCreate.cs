namespace Trident.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharacterID = c.Int(nullable: false, identity: true),
                        CharacterName = c.String(nullable: false, maxLength: 30),
                        CharacterRole = c.String(nullable: false, maxLength: 30),
                        CharacterType = c.String(nullable: false, maxLength: 30),
                        CharacterWeapon = c.Int(nullable: false),
                        HasPic = c.Int(nullable: false),
                        ImgType = c.String(),
                        member_MemberID = c.Int(),
                    })
                .PrimaryKey(t => t.CharacterID)
                .ForeignKey("dbo.Members", t => t.member_MemberID)
                .Index(t => t.member_MemberID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        MemberName = c.String(nullable: false, maxLength: 30),
                        MemberLevel = c.Int(nullable: false),
                        MemberRole = c.String(nullable: false, maxLength: 30),
                        HasPic = c.Int(nullable: false),
                        ImgType = c.String(),
                    })
                .PrimaryKey(t => t.MemberID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "member_MemberID", "dbo.Members");
            DropIndex("dbo.Characters", new[] { "member_MemberID" });
            DropTable("dbo.Members");
            DropTable("dbo.Characters");
        }
    }
}

namespace Trident.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Characters", "HasPic");
            DropColumn("dbo.Characters", "ImgType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Characters", "ImgType", c => c.String());
            AddColumn("dbo.Characters", "HasPic", c => c.Int(nullable: false));
        }
    }
}

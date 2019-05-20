namespace Trident.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TridentMVC : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teams", "TeamMembers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "TeamMembers", c => c.Int(nullable: false));
        }
    }
}

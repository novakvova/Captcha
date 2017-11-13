namespace DBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcolumnsPhototableUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "MinPhoto", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "Photo", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "LargePhoto", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LargePhoto");
            DropColumn("dbo.Users", "Photo");
            DropColumn("dbo.Users", "MinPhoto");
        }
    }
}

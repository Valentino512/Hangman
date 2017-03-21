namespace Hangman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsInHostGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsInHostGame", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsInHostGame");
        }
    }
}

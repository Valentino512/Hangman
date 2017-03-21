namespace Hangman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAuthenticated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsAuthenticated", c => c.Boolean(nullable: false));
        }
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsAuthenticated");
        }
    }
}

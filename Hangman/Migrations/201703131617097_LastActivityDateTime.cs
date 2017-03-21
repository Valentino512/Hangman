namespace Hangman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastActivityDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastActivityDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "IsAuthenticated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsAuthenticated", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "LastActivityDateTime");
        }
    }
}

namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequiredValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Films", "Url", c => c.String(nullable: false));
            AlterColumn("dbo.Films", "Genre", c => c.String(nullable: false));
            AlterColumn("dbo.Films", "Director", c => c.String(nullable: false));
            AlterColumn("dbo.Films", "ReleaseDate", c => c.String(nullable: false));
            AlterColumn("dbo.Films", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Films", "Stars", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "Stars", c => c.String());
            AlterColumn("dbo.Films", "ShortDescription", c => c.String());
            AlterColumn("dbo.Films", "ReleaseDate", c => c.String());
            AlterColumn("dbo.Films", "Director", c => c.String());
            AlterColumn("dbo.Films", "Genre", c => c.String());
            AlterColumn("dbo.Films", "Url", c => c.String());
            AlterColumn("dbo.Clients", "Email", c => c.String());
        }
    }
}

namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedclientIdinMovieRatingsmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieRatings", "clientId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieRatings", "clientId");
        }
    }
}

namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTimeInModelFilm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "Time", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Films", "Time");
        }
    }
}

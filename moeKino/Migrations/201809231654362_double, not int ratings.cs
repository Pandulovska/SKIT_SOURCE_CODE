namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doublenotintratings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "Rating", c => c.Int(nullable: false));
        }
    }
}

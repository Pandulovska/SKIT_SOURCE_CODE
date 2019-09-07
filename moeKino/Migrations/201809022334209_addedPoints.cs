namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Points", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Points");
        }
    }
}

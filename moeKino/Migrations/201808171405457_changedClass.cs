namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedClass : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clients", "Points");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Points", c => c.Int(nullable: false));
        }
    }
}

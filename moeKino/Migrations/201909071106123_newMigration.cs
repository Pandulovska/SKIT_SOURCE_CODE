namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false));
        }
    }
}

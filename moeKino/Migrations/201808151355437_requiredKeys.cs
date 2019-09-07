namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredKeys : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "Name", c => c.String());
        }
    }
}

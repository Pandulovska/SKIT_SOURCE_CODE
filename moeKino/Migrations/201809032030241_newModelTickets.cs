namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newModelTickets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Date", c => c.String());
            AddColumn("dbo.Tickets", "Time", c => c.String());
            AddColumn("dbo.Tickets", "NumberTickets", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "Movie", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "Movie");
            DropColumn("dbo.Tickets", "NumberTickets");
            DropColumn("dbo.Tickets", "Time");
            DropColumn("dbo.Tickets", "Date");
        }
    }
}

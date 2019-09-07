namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNewClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.FilmClients",
                c => new
                    {
                        Film_Id = c.Int(nullable: false),
                        Client_ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Film_Id, t.Client_ClientId })
                .ForeignKey("dbo.Films", t => t.Film_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId, cascadeDelete: true)
                .Index(t => t.Film_Id)
                .Index(t => t.Client_ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmClients", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.FilmClients", "Film_Id", "dbo.Films");
            DropIndex("dbo.FilmClients", new[] { "Client_ClientId" });
            DropIndex("dbo.FilmClients", new[] { "Film_Id" });
            DropTable("dbo.FilmClients");
            DropTable("dbo.Clients");
        }
    }
}

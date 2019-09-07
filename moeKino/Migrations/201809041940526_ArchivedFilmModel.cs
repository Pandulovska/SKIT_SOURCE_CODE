namespace moeKino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArchivedFilmModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArchivedFilms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Url = c.String(),
                        Genre = c.String(),
                        Director = c.String(),
                        ReleaseDate = c.String(),
                        LastScreening = c.String(),
                        Audience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ArchivedFilms");
        }
    }
}

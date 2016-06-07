namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArticles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Tag1 = c.String(),
                        Tag2 = c.String(),
                        Tag3 = c.String(),
                        Author_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.Author_UserId)
                .Index(t => t.Author_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Author_UserId", "dbo.Users");
            DropIndex("dbo.Articles", new[] { "Author_UserId" });
            DropTable("dbo.Articles");
        }
    }
}

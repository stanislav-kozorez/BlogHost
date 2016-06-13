namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentTable2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Article_ArticleId = c.Int(),
                        Author_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleId)
                .ForeignKey("dbo.Users", t => t.Author_UserId)
                .Index(t => t.Article_ArticleId)
                .Index(t => t.Author_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Author_UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "Article_ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "Author_UserId" });
            DropIndex("dbo.Comments", new[] { "Article_ArticleId" });
            DropTable("dbo.Comments");
        }
    }
}

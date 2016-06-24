namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetCascadeOperations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Author_UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "Article_ArticleId", "dbo.Articles");
            DropIndex("dbo.Articles", new[] { "Author_UserId" });
            DropIndex("dbo.Users", new[] { "Role_RoleId" });
            DropIndex("dbo.Comments", new[] { "Article_ArticleId" });
            AlterColumn("dbo.Articles", "Author_UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Role_RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Article_ArticleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "Author_UserId");
            CreateIndex("dbo.Users", "Role_RoleId");
            CreateIndex("dbo.Comments", "Article_ArticleId");
            AddForeignKey("dbo.Articles", "Author_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "Article_ArticleId", "dbo.Articles", "ArticleId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Article_ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "Author_UserId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "Article_ArticleId" });
            DropIndex("dbo.Users", new[] { "Role_RoleId" });
            DropIndex("dbo.Articles", new[] { "Author_UserId" });
            AlterColumn("dbo.Comments", "Article_ArticleId", c => c.Int());
            AlterColumn("dbo.Users", "Role_RoleId", c => c.Int());
            AlterColumn("dbo.Articles", "Author_UserId", c => c.Int());
            CreateIndex("dbo.Comments", "Article_ArticleId");
            CreateIndex("dbo.Users", "Role_RoleId");
            CreateIndex("dbo.Articles", "Author_UserId");
            AddForeignKey("dbo.Comments", "Article_ArticleId", "dbo.Articles", "ArticleId");
            AddForeignKey("dbo.Articles", "Author_UserId", "dbo.Users", "UserId");
        }
    }
}

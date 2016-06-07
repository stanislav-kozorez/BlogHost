namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldTextInArticleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Text");
        }
    }
}

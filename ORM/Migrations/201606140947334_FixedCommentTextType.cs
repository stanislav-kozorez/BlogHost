namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedCommentTextType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Text", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Text", c => c.Int(nullable: false));
        }
    }
}

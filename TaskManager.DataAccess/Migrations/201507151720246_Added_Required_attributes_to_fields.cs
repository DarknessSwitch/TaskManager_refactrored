namespace TaskManager.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Required_attributes_to_fields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Tasks", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Subtasks", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subtasks", "Text", c => c.String());
            AlterColumn("dbo.Tasks", "Text", c => c.String());
            AlterColumn("dbo.Categories", "Text", c => c.String());
        }
    }
}

namespace TaskManager.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Subtasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subtasks", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Subtasks", new[] { "TaskId" });
            DropIndex("dbo.Tasks", new[] { "CategoryId" });
            DropTable("dbo.Subtasks");
            DropTable("dbo.Tasks");
            DropTable("dbo.Categories");
        }
    }
}

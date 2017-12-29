namespace GoldSilver.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowOnMain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jewelries", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Jewelries", new[] { "CategoryId" });
            AddColumn("dbo.Categories", "ShowOnMain", c => c.Boolean());
            AddColumn("dbo.Categories", "ImageData", c => c.Binary());
            AddColumn("dbo.Categories", "ImageMimeType", c => c.String());
            AddColumn("dbo.Categories", "ImageDescription", c => c.String());
            AlterColumn("dbo.Jewelries", "CategoryId", c => c.Int());
            CreateIndex("dbo.Jewelries", "CategoryId");
            AddForeignKey("dbo.Jewelries", "CategoryId", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jewelries", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Jewelries", new[] { "CategoryId" });
            AlterColumn("dbo.Jewelries", "CategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Categories", "ImageDescription");
            DropColumn("dbo.Categories", "ImageMimeType");
            DropColumn("dbo.Categories", "ImageData");
            DropColumn("dbo.Categories", "ShowOnMain");
            CreateIndex("dbo.Jewelries", "CategoryId");
            AddForeignKey("dbo.Jewelries", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}

namespace GoldSilver.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Order = c.Int(),
                        UrlPath = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Gemstones",
                c => new
                    {
                        GemstoneId = c.Int(nullable: false, identity: true),
                        GemstoneName = c.String(),
                        Order = c.Int(),
                        UrlPath = c.String(),
                    })
                .PrimaryKey(t => t.GemstoneId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        ImageDescription = c.String(),
                        JewelryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jewelries", t => t.JewelryId, cascadeDelete: true)
                .Index(t => t.JewelryId);
            
            CreateTable(
                "dbo.Jewelries",
                c => new
                    {
                        JewelryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Article = c.String(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaterialId = c.Int(nullable: false),
                        GemstoneId = c.Int(),
                        Fineness = c.Int(),
                        Popularity = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Set = c.String(),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.JewelryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Gemstones", t => t.GemstoneId)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.GemstoneId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                        Order = c.Int(),
                        UrlPath = c.String(),
                    })
                .PrimaryKey(t => t.MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jewelries", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Images", "JewelryId", "dbo.Jewelries");
            DropForeignKey("dbo.Jewelries", "GemstoneId", "dbo.Gemstones");
            DropForeignKey("dbo.Jewelries", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Jewelries", new[] { "CategoryId" });
            DropIndex("dbo.Jewelries", new[] { "GemstoneId" });
            DropIndex("dbo.Jewelries", new[] { "MaterialId" });
            DropIndex("dbo.Images", new[] { "JewelryId" });
            DropTable("dbo.Materials");
            DropTable("dbo.Jewelries");
            DropTable("dbo.Images");
            DropTable("dbo.Gemstones");
            DropTable("dbo.Categories");
        }
    }
}

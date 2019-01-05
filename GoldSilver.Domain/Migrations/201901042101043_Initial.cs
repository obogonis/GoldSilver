namespace GoldSilver.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        ShowOnMain = c.Boolean(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        ImageDescription = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Jewelries",
                c => new
                    {
                        JewelryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Article = c.String(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        InStock = c.Boolean(nullable: false),
                        Fineness = c.Int(),
                        Popularity = c.Int(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Set = c.String(),
                        VideoFromYoutube = c.String(),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.JewelryId);
            
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
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                        Order = c.Int(),
                        UrlPath = c.String(),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            CreateTable(
                "dbo.JewelryCategories",
                c => new
                    {
                        Jewelry_JewelryId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Jewelry_JewelryId, t.Category_CategoryId })
                .ForeignKey("dbo.Jewelries", t => t.Jewelry_JewelryId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Jewelry_JewelryId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.GemstoneJewelries",
                c => new
                    {
                        Gemstone_GemstoneId = c.Int(nullable: false),
                        Jewelry_JewelryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Gemstone_GemstoneId, t.Jewelry_JewelryId })
                .ForeignKey("dbo.Gemstones", t => t.Gemstone_GemstoneId, cascadeDelete: true)
                .ForeignKey("dbo.Jewelries", t => t.Jewelry_JewelryId, cascadeDelete: true)
                .Index(t => t.Gemstone_GemstoneId)
                .Index(t => t.Jewelry_JewelryId);
            
            CreateTable(
                "dbo.MaterialJewelries",
                c => new
                    {
                        Material_MaterialId = c.Int(nullable: false),
                        Jewelry_JewelryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Material_MaterialId, t.Jewelry_JewelryId })
                .ForeignKey("dbo.Materials", t => t.Material_MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Jewelries", t => t.Jewelry_JewelryId, cascadeDelete: true)
                .Index(t => t.Material_MaterialId)
                .Index(t => t.Jewelry_JewelryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaterialJewelries", "Jewelry_JewelryId", "dbo.Jewelries");
            DropForeignKey("dbo.MaterialJewelries", "Material_MaterialId", "dbo.Materials");
            DropForeignKey("dbo.Images", "JewelryId", "dbo.Jewelries");
            DropForeignKey("dbo.GemstoneJewelries", "Jewelry_JewelryId", "dbo.Jewelries");
            DropForeignKey("dbo.GemstoneJewelries", "Gemstone_GemstoneId", "dbo.Gemstones");
            DropForeignKey("dbo.JewelryCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.JewelryCategories", "Jewelry_JewelryId", "dbo.Jewelries");
            DropIndex("dbo.MaterialJewelries", new[] { "Jewelry_JewelryId" });
            DropIndex("dbo.MaterialJewelries", new[] { "Material_MaterialId" });
            DropIndex("dbo.GemstoneJewelries", new[] { "Jewelry_JewelryId" });
            DropIndex("dbo.GemstoneJewelries", new[] { "Gemstone_GemstoneId" });
            DropIndex("dbo.JewelryCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.JewelryCategories", new[] { "Jewelry_JewelryId" });
            DropIndex("dbo.Images", new[] { "JewelryId" });
            DropTable("dbo.MaterialJewelries");
            DropTable("dbo.GemstoneJewelries");
            DropTable("dbo.JewelryCategories");
            DropTable("dbo.Materials");
            DropTable("dbo.Images");
            DropTable("dbo.Gemstones");
            DropTable("dbo.Jewelries");
            DropTable("dbo.Categories");
        }
    }
}

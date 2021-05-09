namespace cozaStore.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CozaStoreV10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promotion",
                c => new
                    {
                        PromotionId = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PromotionId)
                .ForeignKey("dbo.Product", t => t.PromotionId)
                .Index(t => t.PromotionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotion", "PromotionId", "dbo.Product");
            DropIndex("dbo.Promotion", new[] { "PromotionId" });
            DropTable("dbo.Promotion");
        }
    }
}

namespace cozaStore.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CozaStoreV9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "CommentDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.ProductDetail", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductDetail", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Comment", "CommentDate");
        }
    }
}

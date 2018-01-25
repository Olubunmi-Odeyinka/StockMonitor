namespace StockExchange.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "sm.ActiveStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        PriceTraded = c.Double(nullable: false),
                        PreviousDayPriceTraded = c.Double(nullable: false),
                        Volume = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sm.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId, unique: true, name: "IX_ActiveStock");
            
            CreateTable(
                "sm.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 500),
                        TicketSymbol = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CompanyName, unique: true, name: "IX_Name")
                .Index(t => t.TicketSymbol, unique: true);
            
            CreateTable(
                "sm.UserStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sm.Companies", t => t.CompanyId)
                .Index(t => new { t.CompanyId, t.UserName }, unique: true, name: "IX_CompantUser");
            
        }
        
        public override void Down()
        {
            DropForeignKey("sm.ActiveStocks", "CompanyId", "sm.Companies");
            DropForeignKey("sm.UserStocks", "CompanyId", "sm.Companies");
            DropIndex("sm.UserStocks", "IX_CompantUser");
            DropIndex("sm.Companies", new[] { "TicketSymbol" });
            DropIndex("sm.Companies", "IX_Name");
            DropIndex("sm.ActiveStocks", "IX_ActiveStock");
            DropTable("sm.UserStocks");
            DropTable("sm.Companies");
            DropTable("sm.ActiveStocks");
        }
    }
}

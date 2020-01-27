namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Invoices", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "DateTime", c => c.DateTime(nullable: false));
        }
    }
}

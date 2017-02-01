namespace ClayOnWheels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addActive1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Active", c => c.Int(nullable: false));
        }
    }
}

namespace ClayOnWheels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sfl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Active");
        }
    }
}

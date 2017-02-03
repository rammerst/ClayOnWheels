namespace ClayOnWheels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class joske : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Active");
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean(nullable: false));
            Sql("UPDATE [dbo].[AspNetUsers] SET Active = true");
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Active");
        }
    }
}

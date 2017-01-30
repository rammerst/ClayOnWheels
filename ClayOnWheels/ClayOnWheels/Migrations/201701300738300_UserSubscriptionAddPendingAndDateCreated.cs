namespace ClayOnWheels.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserSubscriptionAddPendingAndDateCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSubscriptions", "Pending", c => c.Int());
            AddColumn("dbo.UserSubscriptions", "Created", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSubscriptions", "Pending");
            DropColumn("dbo.UserSubscriptions", "Created");
        }
    }
}

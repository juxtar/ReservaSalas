namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserva", "Anulada", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reserva", "Fecha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reserva", "Fecha", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reserva", "Anulada");
        }
    }
}

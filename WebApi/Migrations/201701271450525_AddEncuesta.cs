namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEncuesta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserva", "Encuesta_PreparacionYLimpieza", c => c.Int(nullable: false));
            AddColumn("dbo.Reserva", "Encuesta_MaterialesBrindados", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reserva", "Encuesta_ServicioATiempo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reserva", "Encuesta_ServicioAGusto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reserva", "Encuesta_Observacion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reserva", "Encuesta_Observacion");
            DropColumn("dbo.Reserva", "Encuesta_ServicioAGusto");
            DropColumn("dbo.Reserva", "Encuesta_ServicioATiempo");
            DropColumn("dbo.Reserva", "Encuesta_MaterialesBrindados");
            DropColumn("dbo.Reserva", "Encuesta_PreparacionYLimpieza");
        }
    }
}

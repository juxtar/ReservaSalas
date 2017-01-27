namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEncuesta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Encuesta",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PreparacionYLimpieza = c.Int(nullable: false),
                        MaterialesBrindados = c.Boolean(nullable: false),
                        ServicioATiempo = c.Boolean(nullable: false),
                        ServicioAGusto = c.Boolean(nullable: false),
                        Observacion = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Reserva", "Encuesta_ID", c => c.Int());
            CreateIndex("dbo.Reserva", "Encuesta_ID");
            AddForeignKey("dbo.Reserva", "Encuesta_ID", "dbo.Encuesta", "ID");
            DropColumn("dbo.Reserva", "Encuesta_PreparacionYLimpieza");
            DropColumn("dbo.Reserva", "Encuesta_MaterialesBrindados");
            DropColumn("dbo.Reserva", "Encuesta_ServicioATiempo");
            DropColumn("dbo.Reserva", "Encuesta_ServicioAGusto");
            DropColumn("dbo.Reserva", "Encuesta_Observacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reserva", "Encuesta_Observacion", c => c.String());
            AddColumn("dbo.Reserva", "Encuesta_ServicioAGusto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reserva", "Encuesta_ServicioATiempo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reserva", "Encuesta_MaterialesBrindados", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reserva", "Encuesta_PreparacionYLimpieza", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reserva", "Encuesta_ID", "dbo.Encuesta");
            DropIndex("dbo.Reserva", new[] { "Encuesta_ID" });
            DropColumn("dbo.Reserva", "Encuesta_ID");
            DropTable("dbo.Encuesta");
        }
    }
}

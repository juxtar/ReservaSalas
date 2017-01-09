namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ReservaSalas.Modelos;

    public class ReservaSalasDb : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'ReservaSalas' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'DataAccess.ReservaSalas' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'ReservaSalas'  en el archivo de configuración de la aplicación.
        public ReservaSalasDb()
            : base("name=ReservaSalasDb")
        {
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Sala> Salas { get; set; }
        public virtual DbSet<Reserva> Reservas { get; set; }
    }
}
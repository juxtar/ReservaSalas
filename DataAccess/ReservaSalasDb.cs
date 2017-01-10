namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ReservaSalas.Modelos;
    using System.Data.Entity.ModelConfiguration.Conventions;

    // Ejemplo de prueba de implementación de un contexto de base de datos
    public class ReservaSalasDb : DbContext, IDataSession
    {
        public ReservaSalasDb()
            : base("name=ReservaSalasDb")
        {
        }

        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Sala> Salas { get; set; }
        public virtual DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
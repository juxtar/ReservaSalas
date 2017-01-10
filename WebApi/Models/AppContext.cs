using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using DataAccess;
using ReservaSalas.Modelos;

namespace WebApi.Models
{
    public class AppContext : IdentityDbContext<UserModel>, IDataSession
    {
        public AppContext() : base("name=ReservaSalasDb")
        {
            Database.SetInitializer<AppContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static AppContext Create()
        {
            return new AppContext();
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
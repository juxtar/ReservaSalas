namespace WebApi.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApi.Models.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApi.Models.AppContext context)
        {
            var manager = new UserManager<UserModel>(new UserStore<UserModel>(new AppContext()));

            var user = new UserModel()
            {
                UserName = "juanferr",
                Email = "juan@flioh.com",
                EmailConfirmed = true,
                FirstName = "Juan Manuel",
                LastName = "Ferraira",
                Empleado = new ReservaSalas.Modelos.Empleado()
                {
                    Descripcion = "Juan Manuel Ferraira",
                    Legajo = 194
                }
            };

            manager.Create(user, "MySuperP@ssword!");
        }
    }
}

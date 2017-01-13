using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using DataAccess;
using ReservaSalas.Modelos;
using Microsoft.AspNet.Identity;
using ReservaSalas.Servicios;
using ReservaSalas.Excepciones;
using WebApi.Filters;

namespace WebApi.Controllers
{
    public class ReservasController : BaseApiController
    {
        AppContext db;
        ReservasRepository repository;

        public ReservasController()
        {
            db = new AppContext();
            repository = new ReservasRepository(db);
        }

        // GET: api/Reservas
        public IEnumerable<Reserva> Get()
        {
            return repository.Get();
        }

        // GET: api/Reservas/5
        public Reserva Get(int id)
        {
            Reserva reserva;
            if (!repository.TryGet(id, out reserva))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            return reserva;
        }

        // POST: api/Reservas
        [NoExistenteExceptionFilter]
        [ReservaInvalidaExceptionFilter]
        [SalaNoDisponibleExceptionFilter]
        [Authorize]
        public HttpResponseMessage Post([FromBody]Reserva reserva)
        {
            var reservarService = new ReservarService(repository, new EmpleadosRepository(db));
            var nuevaReserva = reservarService.GenerarReserva(reserva,
                    UserManager.FindById(User.Identity.GetUserId())
                        .Empleado_ID.Value);
            var response = Request.CreateResponse(HttpStatusCode.Created, nuevaReserva);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/Sala/" + reserva.ID.ToString());
            return response;
        }

        // PUT: api/Reservas/5
        [NoExistenteExceptionFilter]
        [ReservaInvalidaExceptionFilter]
        [SalaNoDisponibleExceptionFilter]
        [Authorize]
        public void Put(int id, [FromBody]Reserva reserva)
        {
        }

        // DELETE: api/Reservas/5
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}

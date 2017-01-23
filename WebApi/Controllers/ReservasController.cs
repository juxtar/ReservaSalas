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
        ReservarService service;

        public ReservasController()
        {
            db = new AppContext();
            repository = new ReservasRepository(db);
            service = new ReservarService(repository, new EmpleadosRepository(db), new SalasRepository(db));
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

        public IEnumerable<Reserva> Get(
                    int? IdSala,
                    int? IdResponsable,
                    bool? Anulada,
                    bool? Caducada)
        {
            return repository.GetFiltered(IdSala, IdResponsable, Anulada, Caducada);
        }

        // POST: api/Reservas
        [NoExistenteExceptionFilter]
        [ReservaInvalidaExceptionFilter]
        [SalaNoDisponibleExceptionFilter]
        [Authorize]
        public HttpResponseMessage Post([FromBody]Reserva reserva)
        {
            var nuevaReserva = service.GenerarReserva(reserva,
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
        public HttpResponseMessage Put(int id, [FromBody]Reserva reserva)
        {
            var reservaActualizada = service.ActualizarReserva(reserva);
            var response = Request.CreateResponse(HttpStatusCode.OK, reservaActualizada);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/Sala/" + reserva.ID.ToString());
            return response;
        }

        // DELETE: api/Reservas/5
        [NoExistenteExceptionFilter]
        [AnulacionInvalidaExceptionFilter]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            if (!service.AnularReserva(id))
                return BadRequest();
            return Ok();
        }
    }
}

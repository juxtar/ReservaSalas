using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReservaSalas.Modelos;
using WebApi.Models;
using DataAccess;
using ReservaSalas.Excepciones;
using WebApi.Filters;
using ReservaSalas.Servicios;

namespace WebApi.Controllers
{
    public class SalasController : BaseApiController
    {
        AppContext db;
        SalasRepository repository;
        SalasService service;

        public SalasController()
        {
            db = new AppContext();
            repository = new SalasRepository(db);
            service = new SalasService(repository);
        }

        // GET: api/Salas
        [Authorize]
        public IEnumerable<Sala> Get()
        {
            return repository.Get();
        }

        // GET: api/Salas/5
        [NoExistenteExceptionFilter]
        [Authorize]
        public Sala Get(int id)
        {
            Sala sala;
            if (!repository.TryGet(id, out sala))
                throw new NoExistenteException("Sala no existente.");
            return sala;
        }

        // POST: api/Salas
        [YaExistenteExceptionFilter]
        [SalaInvalidaExceptionFilter]
        [Authorize]
        public HttpResponseMessage Post([FromBody]Sala sala)
        {
            sala = service.NuevaSala(sala);
            var response = Request.CreateResponse(HttpStatusCode.Created, sala);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/Sala/" + sala.ID.ToString());
            return response;
        }

        // PUT: api/Salas/5
        [YaExistenteExceptionFilter]
        [NoExistenteExceptionFilter]
        [SalaInvalidaExceptionFilter]
        [Authorize]
        public Sala Put(int id, [FromBody]Sala sala)
        {
            return service.ActualizarSala(sala);
        }

        // DELETE: api/Salas/5
        [NoExistenteExceptionFilter]
        [AnulacionInvalidaExceptionFilter]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            if (!service.EliminarSala(id, new ReservasRepository(db)))
                return BadRequest();
            return Ok();
        }
    }
}

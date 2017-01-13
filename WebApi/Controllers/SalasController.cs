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

namespace WebApi.Controllers
{
    public class SalasController : BaseApiController
    {
        AppContext db;
        SalasRepository repository;

        public SalasController()
        {
            db = new AppContext();
            repository = new SalasRepository(db);
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
        [Authorize]
        public HttpResponseMessage Post([FromBody]Sala sala)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            sala = repository.Add(sala);
            var response = Request.CreateResponse(HttpStatusCode.Created, sala);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/Sala/" + sala.ID.ToString());
            return response;
        }

        // PUT: api/Salas/5
        [YaExistenteExceptionFilter]
        [NoExistenteExceptionFilter]
        [Authorize]
        public Sala Put(int id, [FromBody]Sala sala)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            sala.ID = id;
            if (!repository.Update(sala))
                throw new NoExistenteException("Sala no existente.");
            return sala;
        }

        // DELETE: api/Salas/5
        [NoExistenteExceptionFilter]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            if (!repository.Delete(id))
                throw new NoExistenteException("Sala no existente.");
            return Ok();
        }
    }
}

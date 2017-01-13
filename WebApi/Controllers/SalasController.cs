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
        [Authorize]
        public Sala Get(int id)
        {
            Sala sala;
            if (!repository.TryGet(id, out sala))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            return sala;
        }

        // POST: api/Salas
        [Authorize]
        public HttpResponseMessage Post([FromBody]Sala sala)
        {
            try
            {
                sala = repository.Add(sala);
            }
            catch (YaExistenteException e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Conflict, e));
            }
            var response = Request.CreateResponse(HttpStatusCode.Created, sala);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/Sala/" + sala.ID.ToString());
            return response;
        }

        // PUT: api/Salas/5
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Salas/5
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}

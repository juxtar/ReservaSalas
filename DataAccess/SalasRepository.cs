using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Interfaces;
using ReservaSalas.Modelos;
using ReservaSalas.Excepciones;

namespace DataAccess
{
    public class SalasRepository : ISalasRepository
    {
        IDataSession db;

        public SalasRepository(IDataSession context)
        {
            db = context;
        }

        public Sala Add(Sala sala)
        {
            var consulta = from s in db.Set<Sala>()
                           where s.Nombre == sala.Nombre
                           select s;
            if (consulta.Any())
                throw new YaExistenteException("Ya existe una sala con el mismo nombre.");
            Sala retorno = db.Set<Sala>().Add(sala);
            db.SaveChanges();
            return retorno;
        }

        public bool Delete(int id)
        {
            Sala sala = db.Set<Sala>().Find(id);
            if (sala != null)
            {
                db.Set<Sala>().Remove(sala);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Sala> Get()
        {
            return db.Set<Sala>().ToArray();
        }

        public bool TryGet(int id, out Sala sala)
        {
            sala = db.Set<Sala>().Find(id);
            return sala != null;
        }

        public bool Update(Sala sala)
        {
            var original = db.Set<Sala>().Find(sala.ID);

            if (original != null)
            {
                var consulta = from s in db.Set<Sala>()
                               where s.Nombre == sala.Nombre
                                  && s.ID != sala.ID
                               select s;
                if (consulta.Any())
                    throw new YaExistenteException("Ya existe una sala con el mismo nombre.");
                db.Entry(original).CurrentValues.SetValues(sala);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

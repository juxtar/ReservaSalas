using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Interfaces;
using ReservaSalas.Modelos;

namespace DataAccess
{
    public class SalasRepository : ISalasRepository
    {
        ReservaSalasDb db;

        public SalasRepository()
        {
            db = new ReservaSalasDb();
        }

        public Sala Add(Sala sala)
        {
            Sala retorno = db.Salas.Add(sala);
            db.SaveChanges();
            return retorno;
        }

        public bool Delete(int id)
        {
            Sala sala = db.Salas.Find(id);
            if (sala != null)
            {
                db.Salas.Remove(sala);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Sala> Get()
        {
            return db.Salas.ToArray();
        }

        public bool TryGet(int id, out Sala sala)
        {
            sala = db.Salas.Find(id);
            return sala != null;
        }

        public bool Update(Sala sala)
        {
            var original = db.Salas.Find(sala.ID);

            if (original != null)
            {
                db.Entry(original).CurrentValues.SetValues(sala);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

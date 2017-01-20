using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Interfaces;
using ReservaSalas.Modelos;
using System.Data.Entity;

namespace DataAccess
{
    public class ReservasRepository : IReservasRepository
    {
        IDataSession db;

        public ReservasRepository(IDataSession context)
        {
            db = context;
        }

        public Reserva Add(Reserva reserva)
        {
            Reserva retorno = db.Set<Reserva>().Add(reserva);
            db.SaveChanges();
            return retorno;
        }

        public bool Delete(int id)
        {
            Reserva reserva = db.Set<Reserva>().Find(id);
            if (reserva != null)
            {
                db.Set<Reserva>().Remove(reserva);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Reserva> Get()
        {
            return db.Set<Reserva>()
                .Include(r => r.Sala)
                .Include(r => r.Responsable)
                .ToArray();
        }

        public bool TryGet(int id, out Reserva reserva)
        {
            reserva = db.Set<Reserva>().Find(id);
            db.Entry(reserva).Reference(r => r.Sala).Load();
            db.Entry(reserva).Reference(r => r.Responsable).Load();
            return reserva != null;
        }

        public bool Update(Reserva reserva)
        {
            var original = db.Set<Reserva>().Find(reserva.ID);

            if (original != null)
            {
                db.Entry(original).CurrentValues.SetValues(reserva);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

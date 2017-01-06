using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Interfaces;
using ReservaSalas.Modelos;

namespace DataAccess
{
    public class ReservasRepository : IReservasRepository
    {
        ReservaSalasDb db;

        public ReservasRepository()
        {
            db = new ReservaSalasDb();
        }

        public Reserva Add(Reserva reserva)
        {
            Reserva retorno = db.Reservas.Add(reserva);
            db.SaveChanges();
            return retorno;
        }

        public bool Delete(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            if (reserva != null)
            {
                db.Reservas.Remove(reserva);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Reserva> Get()
        {
            return db.Reservas.ToArray();
        }

        public bool TryGet(int id, out Reserva reserva)
        {
            reserva = db.Reservas.Find(id);
            return reserva != null;
        }

        public bool Update(Reserva reserva)
        {
            var original = db.Reservas.Find(reserva.ID);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Modelos;

namespace ReservaSalas.Interfaces
{
    public interface IReservasRepository
    {
        IEnumerable<Reserva> Get();
        bool TryGet(int id, out Reserva reserva);
        Reserva Add(Reserva reserva);
        bool Delete(int id);
        bool Update(Reserva reserva);
    }
}

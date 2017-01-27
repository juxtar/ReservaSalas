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
        IEnumerable<Reserva> GetFiltered(int? idSala, int? idResponsable,
                    bool? anulada, bool? caducada, bool? encuestada);
        bool TryGet(int id, out Reserva reserva);
        Reserva Add(Reserva reserva);
        bool Delete(int id);
        bool Update(Reserva reserva);
    }
}

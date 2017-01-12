using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Modelos;
using ReservaSalas.Excepciones;
using ReservaSalas.Interfaces;

namespace ReservaSalas.Servicios
{
    public class AnularReservaService
    {
        IReservasRepository repository;

        public AnularReservaService(IReservasRepository repo)
        {
            repository = repo;
        }

        public bool Anular(Reserva r)
        {
            if (!ValidarAnulacion(r))
                return false;
            r.Anulada = true;
            return repository.Update(r);
        }

        private bool ValidarAnulacion(Reserva r)
        {
            if (r.Anulada)
                throw new AnulacionInvalidaException("La reserva ya está anulada.");
            if (r.Fin <= DateTime.Now)
                throw new AnulacionInvalidaException("No puede anular una reserva ya concluída.");
            return true;
        }
    }
}

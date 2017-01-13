using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Interfaces;
using ReservaSalas.Modelos;
using ReservaSalas.Excepciones;

namespace ReservaSalas.Servicios
{
    public class ValidarReservaService
    {
        IReservasRepository repository;
        
        public ValidarReservaService(IReservasRepository repo)
        {
            repository = repo;
        }

        public void Validar(Reserva r)
        {
            if (!ValidarSala(r))
                throw new NoExistenteException("Sala no existente.");
            if (!ValidarResponsable(r))
                throw new NoExistenteException("Responsable no existente.");
            if (!ValidarMotivo(r))
                throw new ReservaInvalidaException("Debe especificar un motivo.");
            if (!ValidarCantidadNoNula(r))
                throw new ReservaInvalidaException("Debe especificar la cantidad de personas.");
            if (!ValidarInicio(r))
                throw new ReservaInvalidaException("La reserva debe ser posterior a la fecha actual.");
            if (!ValidarFin(r))
                throw new ReservaInvalidaException("La reserva debe ser de al menos 1 hora.");
            if (!ValidarServicios(r))
                throw new ReservaInvalidaException("La sala especificada no cumple con servicios ni almuerzo.");
            if (!ValidarCantidadMaxima(r))
                throw new ReservaInvalidaException("La sala no admite la cantidad de personas especificada.");
            if (!ValidarDisponibilidad(r))
                throw new SalaNoDisponibleException("La sala especificada ya se encuentra reservada para ese horario.");
        }
        
        private bool ValidarDisponibilidad(Reserva reserva)
        {
            var reservas = repository.Get();
            var query = from r in reservas
                        where  !r.Anulada
                            && r.Sala == reserva.Sala
                            && r.Inicio < reserva.Fin
                            && r.Fin > reserva.Inicio
                        select r;
            return query.Any();
        }

        private bool ValidarCantidadMaxima(Reserva r)
        {
            return r.Cantidad <= r.Sala.Capacidad;
        }

        private bool ValidarCantidadNoNula(Reserva r)
        {
            return r.Cantidad > 0;
        }

        private bool ValidarServicios(Reserva r)
        {
            return !(r.Servicio || r.Almuerzo) || (r.Sala.Tipo == TipoSala.Reunion);
        }

        private bool ValidarInicio(Reserva r)
        {
            return r.Inicio > DateTime.Now;
        }

        private bool ValidarFin(Reserva r)
        {
            return r.Fin > r.Inicio.AddHours(1);
        }

        private bool ValidarSala(Reserva r)
        {
            return r.Sala != null;
        }

        private bool ValidarResponsable(Reserva r)
        {
            return r.Responsable != null;
        }

        private bool ValidarMotivo(Reserva r)
        {
            return r.Motivo.Trim().Any();
        }
    }
}

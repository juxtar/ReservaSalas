using ReservaSalas.Interfaces;
using ReservaSalas.Modelos;
using ReservaSalas.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Servicios
{
    public class ReservarService
    {
        IReservasRepository ReservasRep;
        IEmpleadosRepository EmpleadosRep;
        ISalasRepository SalasRep;

        public ReservarService(
            IReservasRepository ReservasRepository,
            IEmpleadosRepository EmpleadosRepository,
            ISalasRepository SalasRepository)
        {
            ReservasRep = ReservasRepository;
            EmpleadosRep = EmpleadosRepository;
            SalasRep = SalasRepository;
        }

        public Reserva GenerarReserva(Reserva r, int idEmpleado)
        {
            Empleado responsable;
            if (!EmpleadosRep.TryGet(idEmpleado, out responsable))
                throw new NoExistenteException("Empleado no existente.");
            r.Responsable = responsable;
            Sala sala;
            if (!SalasRep.TryGet(r.Sala.ID, out sala))
                throw new NoExistenteException("Sala no existente.");
            r.Sala = sala;
            var validarSvc = new ValidarReservaService(ReservasRep);
            validarSvc.Validar(r);
            return ReservasRep.Add(r);
        }

        public Reserva ActualizarReserva(Reserva r, int idEmpleado, bool validar = true)
        {
            var validarSvc = new ValidarReservaService(ReservasRep);
            if (r.Responsable.ID != idEmpleado)
                throw new ReservaInvalidaException("Sólo puede actualizar las reservas hechas por su usuario.");
            Sala sala;
            if (!SalasRep.TryGet(r.Sala.ID, out sala))
                throw new NoExistenteException("Sala no existente.");
            if (validar)
                validarSvc.Validar(r);
            if (!ReservasRep.Update(r))
                throw new NoExistenteException("Reserva no existente.");
            return r;
        }
        
        public bool AnularReserva(int idReserva)
        {
            Reserva reserva;
            if (!ReservasRep.TryGet(idReserva, out reserva))
                throw new NoExistenteException("Reserva no existente.");
            var anularSvc = new AnularReservaService(ReservasRep);
            return anularSvc.Anular(reserva);
        }

        public Reserva AgregarEncuesta(int idReserva, Encuesta encuesta, int idEmpleado)
        {
            Reserva reserva;
            if (!ReservasRep.TryGet(idReserva, out reserva))
                throw new NoExistenteException("Reserva no existente.");
            if (reserva.Encuesta != null)
                throw new EncuestaInvalidaException("Ya se realizó la encuesta para esta reserva.");
            if (reserva.Fin > DateTime.Now)
                throw new EncuestaInvalidaException("La encuesta se debe realizar una vez caducada la reserva.");
            if (reserva.Anulada)
                throw new EncuestaInvalidaException("No se puede realizar una encuesta sobre una reserva anulada.");
            reserva.Encuesta = encuesta;
            return ActualizarReserva(reserva, idEmpleado, false);
        }
    }
}

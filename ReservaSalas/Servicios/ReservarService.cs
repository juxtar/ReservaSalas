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

        public Reserva ActualizarReserva(Reserva r)
        {
            var validarSvc = new ValidarReservaService(ReservasRep);
            Sala sala;
            if (!SalasRep.TryGet(r.Sala.ID, out sala))
                throw new NoExistenteException("Sala no existente.");
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
            if (!anularSvc.Anular(reserva))
                return false;
            return true;
        }
    }
}

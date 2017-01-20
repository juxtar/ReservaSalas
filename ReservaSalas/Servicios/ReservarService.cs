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

        public ReservarService(IReservasRepository ReservasRepository, IEmpleadosRepository EmpleadosRepository)
        {
            ReservasRep = ReservasRepository;
            EmpleadosRep = EmpleadosRepository;
        }

        public Reserva GenerarReserva(Reserva r, int idEmpleado)
        {
            Empleado responsable;
            if (!EmpleadosRep.TryGet(idEmpleado, out responsable))
                throw new NoExistenteException("Empleado no existente.");
            r.Responsable = responsable;

            var validarSvc = new ValidarReservaService(ReservasRep);
            validarSvc.Validar(r);
            return ReservasRep.Add(r);
        }

        public Reserva ActualizarReserva(Reserva r)
        {
            var validarSvc = new ValidarReservaService(ReservasRep);
            validarSvc.Validar(r);
            if (!ReservasRep.Update(r))
                throw new NoExistenteException("Reserva no existente.");
            return r;
        }
    }
}

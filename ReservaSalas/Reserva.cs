using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas
{
    class Reserva
    {
        public DateTime Fecha { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public Empleado Responsable { get; set; }
        public int Cantidad { get; set; }
        public Sala Sala { get; set; }
        public string Motivo { get; set; }
        public bool Servicio { get; set; } // Sólo permitido en Salas Reunion
        public bool Almuerzo { get; set; } // Sólo permitido en Salas Reunion
        public bool Proyector { get; set; }
    }
}

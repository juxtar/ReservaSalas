using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas
{
    enum TipoSala
    {
        Capacitacion,
        Reunion,
        Auditorio
    }

    class Sala
    {
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public string Ubicacion { get; set; }
        public TipoSala Tipo { get; set; }
    }
}

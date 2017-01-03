using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Modelos
{
    enum TipoSala
    {
        Capacitacion,
        Reunion,
        Auditorio
    }

    class Sala
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public string Ubicacion { get; set; }
        public TipoSala Tipo { get; set; }

        public Sala() { }

        public Sala(string Nombre, int Capacidad, string Ubicacion, TipoSala Tipo)
        {
            this.Nombre = Nombre;
            this.Capacidad = Capacidad;
            this.Ubicacion = Ubicacion;
            this.Tipo = Tipo;
        }
    }
}

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

        public Sala(string nombre, int capacidad, string ubicacion, TipoSala tipo)
        {
            this.Nombre = nombre;
            this.Capacidad = capacidad;
            this.Ubicacion = ubicacion;
            this.Tipo = tipo;
        }
    }
}

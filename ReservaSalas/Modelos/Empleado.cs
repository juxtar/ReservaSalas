using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Modelos
{
    class Empleado
    {
        public int ID { get; set; }
        public int Legajo { get; set; }
        public string Descripcion { get; set; }

        public Empleado() { }

        public Empleado(int Legajo, string Descripcion)
        {
            this.Legajo = Legajo;
            this.Descripcion = Descripcion;
        }
    }
}

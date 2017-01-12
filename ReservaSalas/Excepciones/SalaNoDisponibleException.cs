using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Excepciones
{
    public class SalaNoDisponibleException : Exception
    {
        public SalaNoDisponibleException()
        {
        }

        public SalaNoDisponibleException(string message) : base(message)
        {
        }

        public SalaNoDisponibleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

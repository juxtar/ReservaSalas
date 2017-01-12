using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Excepciones
{
    public class ReservaInvalidaException : Exception
    {
        public ReservaInvalidaException()
        {
        }

        public ReservaInvalidaException(string message) : base(message)
        {
        }

        public ReservaInvalidaException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

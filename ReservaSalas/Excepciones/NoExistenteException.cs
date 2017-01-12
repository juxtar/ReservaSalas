using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Excepciones
{
    public class NoExistenteException : Exception
    {
        public NoExistenteException()
        {
        }

        public NoExistenteException(string message) : base(message)
        {
        }

        public NoExistenteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

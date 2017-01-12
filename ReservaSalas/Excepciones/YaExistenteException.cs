using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Excepciones
{
    public class YaExistenteException : Exception
    {
        public YaExistenteException()
        {
        }

        public YaExistenteException(string message) : base(message)
        {
        }

        public YaExistenteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

using System;

namespace ReservaSalas.Excepciones
{
    public class SalaInvalidaException : Exception
    {
        public SalaInvalidaException()
        {
        }

        public SalaInvalidaException(string message) : base(message)
        {
        }

        public SalaInvalidaException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

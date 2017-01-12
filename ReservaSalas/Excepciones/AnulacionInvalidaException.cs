using System;

namespace ReservaSalas.Excepciones
{
    public class AnulacionInvalidaException : Exception
    {
        public AnulacionInvalidaException()
        {
        }

        public AnulacionInvalidaException(string message) : base(message)
        {
        }

        public AnulacionInvalidaException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
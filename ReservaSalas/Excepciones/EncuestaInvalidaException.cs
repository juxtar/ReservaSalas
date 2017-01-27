using System;

namespace ReservaSalas.Excepciones
{
    public class EncuestaInvalidaException : Exception
    {
        public EncuestaInvalidaException()
        {
        }

        public EncuestaInvalidaException(string message) : base(message)
        {
        }

        public EncuestaInvalidaException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

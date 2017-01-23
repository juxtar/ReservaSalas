using ReservaSalas.Interfaces;
using ReservaSalas.Excepciones;
using ReservaSalas.Modelos;
using System.Linq;

namespace ReservaSalas.Servicios
{
    public class SalasService
    {
        ISalasRepository repository;

        public SalasService(ISalasRepository SalasRepository)
        {
            repository = SalasRepository;
        }

        public Sala NuevaSala(Sala sala)
        {
            ValidarSala(sala);
            return repository.Add(sala);
        }

        public Sala ActualizarSala(Sala sala)
        {
            ValidarSala(sala);
            if (!repository.Update(sala))
                throw new NoExistenteException("Sala no existente.");
            return sala;
        }

        public bool EliminarSala(int idSala, IReservasRepository reservasRepo)
        {
            var reservasSala = reservasRepo.GetFiltered(idSala, null, null, null);
            if (reservasSala.Any())
                throw new AnulacionInvalidaException(
                    "No se puede eliminar la sala porque ya contiene reservas registradas.");
            if (!repository.Delete(idSala))
                throw new NoExistenteException("Sala no existente");
            return true;
        }

        public void ValidarSala(Sala s)
        {
            if (!ValidarNombre(s))
                throw new SalaInvalidaException("Debe ingresar un nombre para la sala.");
            if (!ValidarNombreExistente(s))
                throw new YaExistenteException("Ya existe una sala con el nombre ingresado.");
            if (!ValidarCapacidad(s))
                throw new SalaInvalidaException("La capacidad debe ser mayor que 0.");
            if (!ValidarUbicacion(s))
                throw new SalaInvalidaException("Debe ingresar una ubicación para la sala.");
        }

        public bool ValidarNombre(Sala s)
        {
            return s.Nombre.Trim().Any();
        }

        public bool ValidarNombreExistente(Sala sala)
        {
            var query = from s in repository.Get()
                        where s.Nombre == sala.Nombre
                              && s.ID != sala.ID
                        select s;
            return !query.Any();
        }

        public bool ValidarCapacidad(Sala s)
        {
            return s.Capacidad > 0;
        }

        public bool ValidarUbicacion(Sala s)
        {
            return s.Ubicacion.Trim().Any();
        }
    }
}

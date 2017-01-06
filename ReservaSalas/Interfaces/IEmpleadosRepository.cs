using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Modelos;

namespace ReservaSalas.Interfaces
{
    public interface IEmpleadosRepository
    {
        IEnumerable<Empleado> Get();
        bool TryGet(int id, out Empleado empleado);
        Empleado Add(Empleado empleado);
        bool Delete(int id);
        bool Update(Empleado empleado);
    }
}

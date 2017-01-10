using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Interfaces;
using ReservaSalas.Modelos;

namespace DataAccess
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        IDataSession db;

        public EmpleadosRepository(IDataSession context)
        {
            db = context;
        }

        public Empleado Add(Empleado empleado)
        {
            Empleado retorno = db.Set<Empleado>().Add(empleado);
            db.SaveChanges();
            return retorno;
        }

        public bool Delete(int id)
        {
            Empleado empleado = db.Set<Empleado>().Find(id);
            if (empleado != null)
            {
                db.Set<Empleado>().Remove(empleado);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Empleado> Get()
        {
            return db.Set<Empleado>().ToArray();
        }

        public bool TryGet(int id, out Empleado empleado)
        {
            empleado = db.Set<Empleado>().Find(id);
            return empleado != null;
        }

        public bool Update(Empleado empleado)
        {
            var original = db.Set<Empleado>().Find(empleado.ID);

            if (original != null)
            {
                db.Entry(original).CurrentValues.SetValues(empleado);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

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
        ReservaSalasDb db;

        public EmpleadosRepository()
        {
            db = new ReservaSalasDb();
        }

        public Empleado Add(Empleado empleado)
        {
            Empleado retorno = db.Empleados.Add(empleado);
            db.SaveChanges();
            return retorno;
        }

        public bool Delete(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            if (empleado != null)
            {
                db.Empleados.Remove(empleado);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Empleado> Get()
        {
            return db.Empleados.ToArray();
        }

        public bool TryGet(int id, out Empleado empleado)
        {
            empleado = db.Empleados.Find(id);
            return empleado != null;
        }

        public bool Update(Empleado empleado)
        {
            var original = db.Empleados.Find(empleado.ID);

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

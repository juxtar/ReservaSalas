using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaSalas.Modelos;

namespace ReservaSalas.Interfaces
{
    public interface ISalasRepository
    {
        IEnumerable<Sala> Get();
        bool TryGet(int id, out Sala sala);
        Sala Add(Sala sala);
        bool Delete(int id);
        bool Update(Sala sala);
    }
}

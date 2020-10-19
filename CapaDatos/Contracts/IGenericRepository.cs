using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        int Create(T entidad);
        List<T> Read();
        int Update(T entidad);
        int Delete(int idPK);
    }
}

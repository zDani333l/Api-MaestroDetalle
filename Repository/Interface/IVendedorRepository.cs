using Api_MaestroDetalle.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Repository.Interface
{
    public interface IVendedorRepository: IRepository<Vendedor>
    {
         Task<IEnumerable<Vendedor>> GetVendedoresByIdCiudad(int IdCiudad);
    }
}

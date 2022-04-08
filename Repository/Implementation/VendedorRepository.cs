using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Repository.Implementation
{
    public class VendedorRepository : BaseRepository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(DBPruebaTecnicaContext context) : base(context) { }

        public async Task<IEnumerable<Vendedor>> GetVendedoresByIdCiudad(int idCiudad)
        {
            return await _entities.Where(x => x.IdCiudad == idCiudad).ToListAsync();
        }

        
    }
}

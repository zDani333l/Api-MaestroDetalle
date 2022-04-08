using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Utils;
using Api_MaestroDetalle.Utils.QueryFilters;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Services.Interface
{
    public interface IVendedorService
    {
        PagedList<Vendedor> GetVendedores(VendedorQueryFilter filters);

        Task<Vendedor> GetVendedor(int id);

        Task<Vendedor> InsertVendedor(Vendedor vendedor);

        Task<bool> UpdateVendedor(Vendedor vendedor);

        Task<bool> DeleteVendedor(int id);
    }
}

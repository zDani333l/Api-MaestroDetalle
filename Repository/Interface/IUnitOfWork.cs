using Api_MaestroDetalle.Models;
using System;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IVendedorRepository VendedorRepository { get; }
        IRepository<Ciudad> CiudadRepository { get; }
        void SaveChanges();

        Task SaveChangesAsync();
    }
}

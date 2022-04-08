using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Repository.Interface;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Repository.Implementation
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DBPruebaTecnicaContext _context;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IRepository<Ciudad> _ciudadRepository;
        public UnitOfWork(DBPruebaTecnicaContext context)
        {
            _context = context;
        }

        public IVendedorRepository VendedorRepository => _vendedorRepository ?? new VendedorRepository(_context);

        public IRepository<Ciudad> CiudadRepository => _ciudadRepository ?? new BaseRepository<Ciudad>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

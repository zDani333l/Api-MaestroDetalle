using Api_MaestroDetalle.Exceptions;
using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Repository.Interface;
using Api_MaestroDetalle.Services.Interface;
using Api_MaestroDetalle.Utils;
using Api_MaestroDetalle.Utils.QueryFilters;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Services.Implemetation
{
    public class VendedorService : IVendedorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public VendedorService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public PagedList<Vendedor> GetVendedores(VendedorQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var vendedores = _unitOfWork.VendedorRepository.GetAll();

            if (filters.Id != null)
            {
                vendedores = vendedores.Where(p => p.Id == filters.Id);
            }
            if (filters.Nombre != null)
            {
                vendedores = vendedores.Where(p => p.Nombre.Contains(filters.Nombre));
            }
            if (filters.Apellido != null)
            {
                vendedores = vendedores.Where(p => p.Apellido.Contains(filters.Apellido));
            }
            if (filters.NumeroIdentificación != null)
            {
                vendedores = vendedores.Where(p => p.NumeroIdentificacion.Contains(filters.NumeroIdentificación));
            }
            if (filters.CiudadId != null)
            {
                vendedores = vendedores.Where(p => p.IdCiudad == filters.CiudadId);
            }
            var pagedVendedores = PagedList<Vendedor>.Create(vendedores, filters.PageNumber, filters.PageSize);
            return pagedVendedores;
        }

        public async Task<Vendedor> GetVendedor(int id)
        {
            var result =  await _unitOfWork.VendedorRepository.GetById(id);
            if (result == null)
            {
                throw new ExceptionApi("Vendedor no encontrado",404);
            }
            return result;
        }
        
        public async Task<Vendedor> InsertVendedor(Vendedor vendedor)
        {
            vendedor.Id = 0;
            if(vendedor.IdCiudad == null)
            {
                throw new ExceptionApi("Falta agregar la ciudad a la que pertenece el vendedor",404);
            } 
            var ciudad = await _unitOfWork.CiudadRepository.GetById(int.Parse(""+vendedor.IdCiudad));
            if (ciudad == null)
            {
                throw new ExceptionApi("La ciudad ingresada no existe",404);
            }
            await _unitOfWork.VendedorRepository.Add(vendedor);
            await _unitOfWork.SaveChangesAsync();
            return await _unitOfWork.VendedorRepository.GetLatest();
        }

        public async Task<bool> UpdateVendedor(Vendedor vendedor)
        {
          
            var existingVendedor = await this.GetVendedor(vendedor.Id);
            existingVendedor.Nombre = vendedor.Nombre;
            existingVendedor.Apellido = vendedor.Apellido;
            existingVendedor.NumeroIdentificacion = vendedor.NumeroIdentificacion;
            existingVendedor.IdCiudad = vendedor.IdCiudad;

            _unitOfWork.VendedorRepository.Update(existingVendedor);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVendedor(int id)
        {
            await this.GetVendedor(id);
            await _unitOfWork.VendedorRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

       
    }
}

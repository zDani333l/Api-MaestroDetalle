using Api_MaestroDetalle.Exceptions;
using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Repository.Interface;
using Api_MaestroDetalle.Services.Implemetation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Services.Interface
{
    public class CiudadService : ICiudadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CiudadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public IEnumerable<Ciudad> GetCiudades()
        {
            return _unitOfWork.CiudadRepository.GetAll();
        }

        public async Task<Ciudad> GetCiudad(int id)
        {
            var result = await _unitOfWork.CiudadRepository.GetById(id);
            if (result == null)
            {
                throw new ExceptionApi("Ciudad no registrada",410);
            }
            return result;
        }

        public async Task<Ciudad> InsertCiudad(Ciudad ciudad)
        {
            ciudad.Id = 0;
            await _unitOfWork.CiudadRepository.Add(ciudad);
            await _unitOfWork.SaveChangesAsync();
            return await _unitOfWork.CiudadRepository.GetLatest();
        }

        public async Task<bool> UpdateCiudad(Ciudad ciudad)
        {
            var existingCiudad = await this.GetCiudad(ciudad.Id);
            existingCiudad.Descripcion = ciudad.Descripcion;

            _unitOfWork.CiudadRepository.Update(existingCiudad);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}

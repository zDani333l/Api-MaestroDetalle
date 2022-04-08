using Api_MaestroDetalle.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Services.Implemetation
{
    public interface ICiudadService
    {
        IEnumerable<Ciudad> GetCiudades();
        Task<Ciudad> GetCiudad(int id);
        Task<Ciudad> InsertCiudad(Ciudad ciudad);
        Task<bool> UpdateCiudad(Ciudad ciudad);
   
    }
}

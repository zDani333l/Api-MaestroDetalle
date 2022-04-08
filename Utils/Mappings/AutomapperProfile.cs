using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Models.Entities;
using AutoMapper;

namespace Api_MaestroDetalle.Mappings
{
    public class AutomapperProfile : Profile
    {
        //METODO PARA MAPEAR LOS MODELOS DEL DOMINO 
        public AutomapperProfile()
        {
            CreateMap<Ciudad, CiudadDTO>();
            CreateMap<CiudadDTO, Ciudad>();

            CreateMap<Vendedor, VendedorDTO>();
            CreateMap<VendedorDTO, Vendedor>();
        }
    }
}

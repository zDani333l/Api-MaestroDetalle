using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Models.Entities;
using Api_MaestroDetalle.Services.Implemetation;
using Api_MaestroDetalle.Utils;
using Api_MaestroDetalle.Utils.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly ICiudadService _ciudadService;
        private readonly IMapper _mapper;


        public CiudadController(ICiudadService ciudadService, IMapper mapper)
        {
            _ciudadService = ciudadService;
            _mapper = mapper;
        }

        [HttpGet]
        public  IActionResult GetCiudades()
        {
            var ciudades = _ciudadService.GetCiudades();
            var ciudadesDTO =  _mapper.Map<CiudadDTO[]>(ciudades);
            var response = new ApiResponse<CiudadDTO[]>(ciudadesDTO);
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCiudad(int id)
        {
            var ciudad = await _ciudadService.GetCiudad(id);
            var ciudadDTO = _mapper.Map<CiudadDTO>(ciudad);
            var response = new ApiResponse<CiudadDTO>(ciudadDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCiudad(CiudadDTO ciudadDTO)
        {
            var ciudad = _mapper.Map<Ciudad>(ciudadDTO);
            var result = await _ciudadService.InsertCiudad(ciudad);
            var ciudadDto = _mapper.Map<CiudadDTO>(result);
            var response = new ApiResponse<CiudadDTO>(ciudadDto);
            return Ok(response); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCiudad(int id, CiudadDTO ciudadDTO)
        {
            var ciudad = _mapper.Map<Ciudad>(ciudadDTO);
            ciudad.Id = id;
            var result = await _ciudadService.UpdateCiudad(ciudad);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}

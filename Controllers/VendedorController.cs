using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Models.Entities;
using Api_MaestroDetalle.Services.Interface;
using Api_MaestroDetalle.Utils;
using Api_MaestroDetalle.Utils.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _vendedorService;
        private readonly IMapper _mapper;

        public VendedorController(IVendedorService vendedorService, IMapper mapper)
        {
            _vendedorService = vendedorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetVendedores([FromQuery] VendedorQueryFilter filters)
        {
            var vendedores = _vendedorService.GetVendedores(filters);
            var vendedoresDtos = _mapper.Map<IEnumerable<VendedorDTO>>(vendedores);
            var metadata = new Metadata
            {
                TotalCount = vendedores.TotalCount,
                PageSize = vendedores.PageSize,
                CurrentPage = vendedores.CurrentPage,
                TotalPages = vendedores.TotalPages,
                HasNextPage = vendedores.HasNextPage,
                HasPreviousPage = vendedores.HasPreviousPage
            };
            var response = new ApiResponse<IEnumerable<VendedorDTO>>(vendedoresDtos)
            {
                Meta = metadata
            };
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendedor(int id)
        {
            var vendedor = await _vendedorService.GetVendedor(id);
            var vendedorDTO = _mapper.Map<VendedorDTO>(vendedor);
            if (vendedorDTO == null)
            {
                return NotFound();
            }
            var response = new ApiResponse<VendedorDTO>(vendedorDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendedor(VendedorDTO vendedorDTO)
        {
            var vendedor = _mapper.Map<Vendedor>(vendedorDTO);
           var result = await _vendedorService.InsertVendedor(vendedor);
            var vendedorDto = _mapper.Map<VendedorDTO>(result);
            var response = new ApiResponse<VendedorDTO>(vendedorDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendedor(int id, VendedorDTO vendedorDTO)
        {
            var vendedor = _mapper.Map<Vendedor>(vendedorDTO);
            vendedor.Id = id;
            var result = await _vendedorService.UpdateVendedor(vendedor);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendedor(int id)
        {
            var result = await _vendedorService.DeleteVendedor(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}

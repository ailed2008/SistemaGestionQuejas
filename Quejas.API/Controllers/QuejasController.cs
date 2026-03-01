using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quejas.Business;
using Quejas.Entities;
namespace Quejas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuejasController : ControllerBase
    {
        private readonly IQuejaService _service;

        public QuejasController(IQuejaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Queja nuevaQueja)
        {
            try
            {
                var resultado = await _service.RegistrarQueja(nuevaQueja);
                return CreatedAtAction(nameof(GetById), new { id = resultado.IdQueja }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var queja = await _service.ObtenerPorId(id);
            if (queja == null) return NotFound(new { mensaje = "Queja no encontrada" });
            return Ok(queja);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? idEstado, [FromQuery] DateTime? inicio, [FromQuery] DateTime? fin, [FromQuery] int? idCategoria)
        {
            
            var listado = await _service.Consultar(idEstado, inicio, fin, idCategoria);
            return Ok(listado);
        }

   
        [HttpPut("{id}/estatus")]
        public async Task<IActionResult> CambiarEstatus(int id, [FromQuery] int nuevoEstadoId, [FromQuery] string usuario, [FromQuery] string? comentario)
        {
            try
            {
                // Este método en el Service debe validar las reglas (ej: no cerrar si no está resuelta)
                await _service.CambiarEstatus(id, nuevoEstadoId, usuario, comentario);
                return Ok(new { mensaje = "Estatus actualizado e historial registrado correctamente" });
            }
            catch (Exception ex)
            {
                // Aquí capturamos las validaciones de negocio (reglas mínimas esperadas)
                return BadRequest(new { mensaje = ex.Message });
            }
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Factura.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api_Factura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetalleFacturaController( ApplicationDbContext   context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearDetalleFactura([FromBody] DetalleFactura detalleFactura)
        {
            if (detalleFactura == null)
            {
                return BadRequest("El detalle de la factura no puede ser nulo.");
            }

            await _context.DetalleFacturas.AddAsync(detalleFactura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerDetalleFactura), new { id = detalleFactura.IdDetalle }, detalleFactura);
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<DetalleFactura>>> ListaDetalleFacturas()
        {
            var detallesFactura = await _context.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.Producto)
                .ToListAsync();
            return Ok(detallesFactura);
        }

        [HttpGet]
        [Route("ver/{id}")]
        public async Task<IActionResult> ObtenerDetalleFactura(long id)
        {
            var detalleFactura = await _context.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(d => d.IdDetalle == id);

            if (detalleFactura == null)
            {
                return NotFound("Detalle de factura no encontrado.");
            }

            return Ok(detalleFactura);
        }

        [HttpPut]
        [Route("editar/{id}")]
        public async Task<IActionResult> ActualizarDetalleFactura(long id, [FromBody] DetalleFactura detalleFactura)
        {
            if (detalleFactura == null || id != detalleFactura.IdDetalle)
            {
                return BadRequest("Datos del detalle de la factura no válidos.");
            }

            var detalleExistente = await _context.DetalleFacturas.FindAsync(id);

            if (detalleExistente == null)
            {
                return NotFound("Detalle de factura no encontrado.");
            }

            detalleExistente.IdFactura = detalleFactura.IdFactura;
            detalleExistente.IdProducto = detalleFactura.IdProducto;
            detalleExistente.Cantidad = detalleFactura.Cantidad;
            detalleExistente.UnidadMedida = detalleFactura.UnidadMedida;
            detalleExistente.Precio = detalleFactura.Precio;
            detalleExistente.IVA = detalleFactura.IVA;
            detalleExistente.Subtotal = detalleFactura.Subtotal;

            _context.DetalleFacturas.Update(detalleExistente);
            await _context.SaveChangesAsync();
            return Ok(detalleExistente);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IActionResult> EliminarDetalleFactura(long id)
        {
            var detalleFactura = await _context.DetalleFacturas.FindAsync(id);

            if (detalleFactura == null)
            {
                return NotFound("Detalle de factura no encontrado.");
            }

            _context.DetalleFacturas.Remove(detalleFactura);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using Api_Factura.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Factura.Controllers
{
    [Route("api/factura")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacturaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/factura/lista
        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<object>>> GetFacturas()
        {
            var facturas = await _context.Facturas
                .Include(f => f.Cliente) // Incluye la información del Cliente
                .Select(f => new
                {
                    f.IdFactura,
                    f.Establecimiento,
                    f.PuntoEmision,
                    f.NumeroFactura,
                    f.Fecha,
                    NombreCliente = f.Cliente != null ? f.Cliente.Nombre : "Desconocido", // Nombre del cliente
                    IdentificacionCliente = f.Cliente != null ? f.Cliente.Identificacion : "Desconocido", // Identificación del cliente
                    IdCliente = f.Cliente != null ? (long?)f.Cliente.IdCliente : null // ID del cliente convertido a long?
                })
                .ToListAsync();

            return Ok(facturas);
        }

        // GET: api/factura/clientes
        [HttpGet("clientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpPost]
        [Route("crearFactura")]
        public async Task<IActionResult> CreateFactura(
            [FromForm] string establecimiento,
            [FromForm] string puntoEmision,
            [FromForm] string numeroFactura,
            [FromForm] DateTime fecha,
            [FromForm] long idCliente)
        {
            // Crear un nuevo objeto Factura con los datos recibidos
            var factura = new Factura
            {
                Establecimiento = establecimiento,
                PuntoEmision = puntoEmision,
                NumeroFactura = numeroFactura,
                Fecha = fecha,
                IdCliente = idCliente
            };

            // Guardar la factura en la base de datos
            await _context.Facturas.AddAsync(factura);
            await _context.SaveChangesAsync();

            // Devolver un mensaje de éxito con el ID de la factura recién creada
            return Ok(new { IdFactura = factura.IdFactura });
        }


        // DELETE: api/factura/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteFactura(long id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Factura.Models;

namespace Api_Factura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crearCliente")]
        public async Task<IActionResult> CrearCliente(
      [FromForm] string identificacion,
      [FromForm] string nombre,
      [FromForm] string direccion,
      [FromForm] string telefono,
      [FromForm] string correo)
        {
            // Crear un nuevo objeto Cliente con los datos recibidos
            var cliente = new Cliente
            {
                Identificacion = identificacion,
                Nombre = nombre,
                Direccion = direccion,
                Telefono = telefono,
                Correo = correo
            };

            // Guardar el cliente en la base de datos
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            // Devolver un mensaje de éxito con el ID del cliente recién creado
            return Ok(new { IdCliente = cliente.IdCliente });
        }


        // GET: api/Clientes/lista
        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Cliente>>> ListaClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        // GET: api/Clientes/ver/5
        [HttpGet]
        [Route("ver/{id}")]
        public async Task<IActionResult> VerCliente(long id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/editar/5
        [HttpPut]
        [Route("editar/{id}")]
        public async Task<IActionResult> ActualizarCliente(long id, [FromBody] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest("El ID del cliente no coincide.");
            }

            var clienteExistente = await _context.Clientes.FindAsync(id);

            if (clienteExistente == null)
            {
                return NotFound();
            }

            clienteExistente.Identificacion = cliente.Identificacion;
            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Direccion = cliente.Direccion;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.Correo = cliente.Correo;

            _context.Entry(clienteExistente).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Clientes/eliminar/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IActionResult> EliminarCliente(long id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

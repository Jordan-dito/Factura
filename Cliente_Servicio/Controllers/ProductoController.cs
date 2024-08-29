
using Cliente_Servicio.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cliente_Factura.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7091/api/producto"); // Cambia la URL base según sea necesario
        }

        // GET: /Producto/Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("lista");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<IEnumerable<ProductoViewModel>>(jsonString);
                return View(productos);
            }

            // Manejar errores de la solicitud
            return View("Error");
        }
    }
}


using Cliente_Servicio.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cliente_Servicio.Controllers
{
    public class FacturaController : Controller
    {
        private readonly HttpClient _httpClient;

        public FacturaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7091/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("factura/lista"); // Cambiado a "factura/lista"

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var facturas = JsonConvert.DeserializeObject<IEnumerable<FacturaViewModel>>(content);

                return View(facturas);
            }

            // Si la respuesta no fue exitosa, se puede mostrar un mensaje de error o una vista vacía
            return View(new List<FacturaViewModel>());
        }


    }
}

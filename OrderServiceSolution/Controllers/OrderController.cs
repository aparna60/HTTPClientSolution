using Microsoft.AspNetCore.Mvc;
using OrderServiceSolution.Model;

namespace OrderServiceSolution.Controllers
{
    [Route("api/order")]
    public class OrderController : Controller
    {
        public readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }
        public async Task<IActionResult> Index()
        {
          var results= await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("http://localhost:5125/api/product");
          return Ok(results);
           
        }
    }
}

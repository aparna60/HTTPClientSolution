using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using OrderServiceSolution.Model;

namespace OrderServiceSolution.Controllers
{

    [Route("api/display")]
    public class DisplayProductsController : ControllerBase
    {
        HttpClient _httpclient;
        ILogger<DisplayProductsController> _logger;

        public DisplayProductsController(IHttpClientFactory httpClientFactory, ILogger<DisplayProductsController> logger)
        {
            _httpclient = httpClientFactory.CreateClient("ProductService");
            _logger = logger;
        }
        public async Task<IActionResult> GetProducts()
        {
            try
            {
               //[Route("api/product")] "This means that when we pass "product" as the relative URL, it gets appended to "http://localhost:5125/api/". 
                var results = await _httpclient.GetFromJsonAsync<IEnumerable<Product>>("product");
                return Ok(results);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error in fetching products: {ex.Message}");
                //return StatusCode(500, "Failed in fetching products, try again later");
                //Better approach
                return Problem(
                    detail: "Failed in fetching products, try again later",
                    statusCode: 500,
                    title: "Service error"
                    );

               
            }
        }
    }
}

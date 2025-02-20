using Microsoft.AspNetCore.Mvc;
using ProductService.Model;

namespace ProductService.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductData _productData;

        public ProductController(IProductData productData)
        {
                _productData = productData;
        }

        public async Task<IActionResult> GetProducts()
        {
            //Task.Delay(10000);
            var results= await _productData.GetProducts();
            return Ok(results);
        }
       
    }
}

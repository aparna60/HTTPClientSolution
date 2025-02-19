using ProductService.Model;

namespace ProductService
{
    public interface IProductData
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}
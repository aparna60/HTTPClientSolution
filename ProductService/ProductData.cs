using ProductService.Model;

namespace ProductService
{
    public class ProductData : IProductData
    {
        public Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products = new List<Product>()
         {
             new Product()
             {
                 ProductId = 1,
                 Name = "Apple",
                 Description="Fruit"
             },
             new Product() {
                 ProductId = 2,
                 Name= "Guava",
                 Description="Fruit"
             },
             new Product()
             {
                 ProductId = 3,
                 Name="Gun",
                 Description="Toy"
             },
             new Product()
             {
                 ProductId = 4,
                 Name="Car",
                 Description="Toy"
             }

        };
            return Task.FromResult(products);
        }
    }

}


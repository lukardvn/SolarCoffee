using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Products
{
    public interface IProductService
    {
        //ServiceResponse<List<Product>> GetAllProducts();
        List<Product> GetAllProducts();
        //ServiceResponse<Product> GetProductById(int id);
        Product GetProductById(int id);
        ServiceResponse<Product> CreateProduct(Product product);
        ServiceResponse<Product> ArchiveProduct(int id);
    }
}

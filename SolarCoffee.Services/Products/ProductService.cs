using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;

        public ProductService(SolarDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Retrieves all the products
        /// </summary>
        /// <returns></returns>
        List<Product> IProductService.GetAllProducts()
        {
            return _db.Products.ToList();
        }

        /// <summary>
        /// Retrieves the product by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product IProductService.GetProductById(int id)
        {
            return _db.Products.Find(id);
        }

        /// <summary>
        /// Adds a new product to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse<Product> CreateProduct(Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _db.ProductInventories.Add(newInventory);

                _db.SaveChanges();

                return new ServiceResponse<Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }


        /// <summary>
        /// Archives a product by setting booleas IsArchived to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Product archived",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }
    }
}

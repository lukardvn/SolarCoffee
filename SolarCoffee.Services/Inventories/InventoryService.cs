using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Inventories
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        public void CreateSnapshot()
        {
            throw new NotImplementedException();
        }

        public ProductInventory GetByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets current inventory
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }
        
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates number of units available of the provided product id
        /// Adjusts QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adjustment"></param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;

            try
            {
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inventory => inventory.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try { CreateSnapshot(); }       
                catch (Exception ex) { _logger.LogError(ex.Message); }
                    
                _db.SaveChanges();
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = now
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Error updating ProductInventory QuantityOnHand",
                    Time = now
                };
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Inventories;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        [HttpGet("/api/inventory")]
        public IActionResult GetInventory()
        {
            _logger.LogInformation("Getting all inventory");
            var inventory = _inventoryService.GetCurrentInventory()  
                .Select(pi => new ProductInventoryModel
                {
                    Id = pi.Id,
                    Product = ProductMapper.SerializeProductModel(pi.Product),
                    IdealQuantity = pi.IdealQuantity,
                    QuantityOnHand = pi.QuantityOnHand
                })
                .OrderBy(inv => inv.Product.Name)
                .ToList();

            return Ok(inventory);
        }

        [HttpPatch("api/inventory")]
        public IActionResult UpdateInventory([FromBody]ShipmentModel shipment)
        {
            _logger.LogInformation($"Updating inventory for product {shipment.ProductId}. Adjustment: {shipment.Adjustment}");

            var inventory = _inventoryService.UpdateUnitsAvailable(shipment.ProductId, shipment.Adjustment);
            return Ok(inventory);
        }
    }
}

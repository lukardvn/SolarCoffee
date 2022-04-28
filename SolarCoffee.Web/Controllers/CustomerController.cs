using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customers;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("/api/customer")]
        public IActionResult CreateCustomer([FromBody]CustomerModel customer)
        {
            _logger.LogInformation("Creating a new customer");

            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);

            return Ok(newCustomer);
        }

        [HttpGet("/api/customer")]
        public IActionResult GetCustomers()
        {
            _logger.LogInformation("Getting customers");
            var customers = _customerService.GetAllCustomers();

            var customerModels = customers.Select(cust => new CustomerModel
            {
                Id = cust.Id,
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                PrimaryAddress = CustomerMapper.MapCustomerAddress(cust.PrimaryAddress),
                CreatedOn = cust.CreatedOn,
                UpdatedOn = cust.UpdatedOn
            })
                .OrderByDescending(cust => cust.CreatedOn)
                .ToList();

            return Ok(customerModels);
        }

        [HttpDelete("/api/customer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting a customer");
            var response = _customerService.DeleteCustomer(id);

            return Ok(response);
        }
    }
}

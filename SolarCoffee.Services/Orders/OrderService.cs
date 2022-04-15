using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Orders
{
    public class OrderService : IOrderService 
    {
        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            throw new NotImplementedException();
        }

        public List<SalesOrder> GetOrders()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Services.Orders
{
    public interface IOrderService
    {
        public List<SalesOrder> GetOrders();
        ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order);
        ServiceResponse<bool> MarkFulfilled(int id);
    }
}

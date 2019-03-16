using System.Linq;
using Core.Domain.Order;
using Core.Model;

namespace Core.Aplication.Queries.OrderDetails
{
    public class OrderDetailsQuery
    {
        private readonly SahuarOSContext _context;

        public OrderDetailsQuery(SahuarOSContext context)
        {
            _context = context;
        }

        public OrderDetailsResult Execute(int id)
        {
            return _context.Orders.Where(order => order.Id == id)
                       .Select(o => new OrderDetailsResult
                       {
                           id = o.Id,
                           status = o.Status,
                           products = o.Products.Select(p => new OrderProductDetailsResult()
                           {
                               descripciton = p.Product.Description,
                               id = p.Product.Id,
                               name = p.Product.Name,
                               sku = p.Product.SKU 
                           }).ToList()
                       }).FirstOrDefault() ??
                   new OrderDetailsResult();
        }
    }
}
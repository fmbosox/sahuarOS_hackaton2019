using System.Collections.Generic;
using System.Linq;
using Core.Domain.Order;
using Core.Model;

namespace Core.Aplication.Queries.PendingOrders
{
    public class PendingOrdersQuery
    {
        private readonly SahuarOSContext _context;

        public PendingOrdersQuery(SahuarOSContext context)
        {
            _context = context;
        }

        public IList<PendingOrdersResult> Execute()
        {
            return _context.Orders.Where(order => order.Status != Order.OrderStatus.Finished)
                .Select(order => new PendingOrdersResult
                {
                    createdAt = order.CreatedAt,
                    customer = order.Customer.Name,
                    lastModified = order.LastModified,
                    orderId = order.Id
                }).ToList();
        }
    }
}
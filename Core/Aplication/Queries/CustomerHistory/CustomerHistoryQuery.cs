using System.Linq;
using Core.Model;

namespace Core.Aplication.Queries.CustomerHistory
{
    public class CustomerHistoryQuery
    {
        private readonly SahuarOSContext _context;

        public CustomerHistoryQuery(SahuarOSContext context)
        {
            _context = context;
        }

        public CustomerHistoryResult Execute(int customerId)
        {
            return _context.Customers.Where(customer => customer.Id == customerId)
                       .Select(c => new CustomerHistoryResult
                       {
                           orders = _context.Orders.Where(o => o.Customer.Id == customerId)
                               .Select(o => new OrderHistoryResult
                               {
                                   id = o.Id,
                                   lastModified = o.LastModified,
                                   status = o.Status
                               }).ToList()
                       }).FirstOrDefault() ?? new CustomerHistoryResult();
        }
    }
}
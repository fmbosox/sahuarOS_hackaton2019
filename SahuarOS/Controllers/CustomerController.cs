using System.Linq;
using Core.Aplication.Queries.CustomerHistory;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using SahuarOS.Presenters;

namespace SahuarOS.Controllers
{
    public class CustomerController : Controller
    {
        private readonly SahuarOSEFContext _context;

        public CustomerController(SahuarOSEFContext context)
        {
            _context = context;
        }

        public IActionResult History(int id)
        {
            var query = new CustomerHistoryQuery(_context);
            var result = query.Execute(id).orders.Select(o => new
            {
                o.id,
                lastModified = o.lastModified.ToString("MM/dd/yyyy H:mm"),
                status = OrderPresenter.PresenetStatus(o.status)
            });

            return Json(result);
        }
    }
}
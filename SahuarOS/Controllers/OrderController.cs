using System;
using System.Linq;
using Core.Aplication.NewOrder;
using Core.Aplication.Queries.PendingOrders;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace SahuarOS.Controllers
{
    public class OrderController : Controller
    {
        private readonly SahuarOSEFContext _context;

        public OrderController(SahuarOSEFContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody] JObject body)
        {
            var request = body.ToObject<NewOrderRequest>();
            request.Now = DateTime.Now;
            var useCase = new NewOrderUseCase(_context);
            var response = useCase.CreateOrder(request);

            return Json(response);

        }

        public IActionResult Pending()
        {
            var query = new PendingOrdersQuery(_context);
            var result = query.Execute().Select(order => new
            {
                createdAt = order.createdAt.ToShortDateString(),
                order.customer,
                order.orderId,
                lastModified = order.lastModified.ToShortDateString(),
                order.status
            });

            return Json(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
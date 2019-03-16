using System;
using System.Linq;
using Core.Aplication.NewOrder;
using Core.Aplication.Queries.OrderDetails;
using Core.Aplication.Queries.PendingOrders;
using Infrastructure.Hubs;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

namespace SahuarOS.Controllers
{
    public class OrderController : Controller
    {
        private readonly SahuarOSEFContext _context;
        private readonly IHubContext<OrderHub> _orderHub;

        public OrderController(SahuarOSEFContext context, IHubContext<OrderHub> orderHub)
        {
            _context = context;
            _orderHub = orderHub;
        }

        [HttpPost]
        public IActionResult Create([FromBody] JObject body)
        {
            var request = body.ToObject<NewOrderRequest>();
            request.Now = DateTime.Now;
            var eventDistpacher = new OrderSignalREventDistpacher(_orderHub);
            var useCase = new NewOrderUseCase(_context, eventDistpacher);
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
            var orders = _context.Orders
                .OrderByDescending(order => order.CreatedAt).ToList()
                .Select(order => new
                {
                    id = order.Id,
                    customer = order.Customer.Name,
                    products = order.Products.Select(product => new
                    {
                        id = product.Product.Id,
                        name = product.Product.Name
                    }),
                    progress = order.FinishedPercentage(),
                    creatAt = order.CreatedAt.ToString("MM/dd/yyyy H:mm"),
                    status = order.Status
                });

            ViewBag.context = new
            {
                orders
            };

            return View();
        }

        public ActionResult Production(int id)
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var query = new OrderDetailsQuery(_context);

            return Json(query.Execute(id));
        }
    }
}
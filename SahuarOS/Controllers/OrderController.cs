using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Core.Aplication.NewOrder;
using Core.Aplication.Queries.OrderDetails;
using Core.Aplication.Queries.PendingOrders;
using Core.Aplication.StartProduction.StarProduct;
using Infrastructure.Hubs;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SahuarOS.Presenters;

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
            var o = _context.Orders.Where(order => order.Id == id)
                .ToList().Select(order => new
                {
                    id = order.Id,
                    customer = order.Customer.Name,
                    products = order.Products.Select(product => new
                    {
                        id = product.Product.Id,
                        name = product.Product.Name,
                        sku = product.Product.SKU,
                        description = product.Product.Description,
                        status = product.Status,
                        amount = product.Amount,
                    }),
                    progress = order.FinishedPercentage(),
                    creatAt = order.CreatedAt.ToString("MM/dd/yyyy H:mm"),
                    status = order.Status
                }).First();

            ViewBag.context = new
            {
                order = o
            };
            return View();
        }

        public ActionResult Details(int id)
        {
            var query = new OrderDetailsQuery(_context);
            var result = query.Execute(id);

            return Json(new
            {
                result.id,
                status =  OrderPresenter.PresenetStatus(result.status),
                creatAt = result.createdAt.ToString("MM/dd/yyyy H:mm"),
                products = result.products.Select(p => new
                {
                    p.id,
                    p.name,
                    p.sku,
                    p.descripciton,
                    status = OrderPresenter.PresentStatus(p.status)
                })
            });
        }

        [HttpPost]
        public ActionResult StartProduct([FromBody] JObject body)
        {
            var request = body.ToObject<StartProductoRequest>();
            request.Now = DateTime.Now;
            var useCase = new StartProductoUseCase(_context);
            var result = useCase.Start(request);

            return Ok();
        }

        [HttpPost]
        public ActionResult FinishProduct([FromBody] JObject body)
        {
            var request = body.ToObject<StartProductoRequest>();
            request.Now = DateTime.Now;

            var order = _context.Orders.Find(request.OrderId);
            order.FinishProduct(request.ProductId, request.Now);
            try
            {
                _context.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
            }


            return Ok();
        }

        public ActionResult GCode(int id)
        {
            return File(_context.Products.First().GCode, "text/gcode");
        }
    }
}
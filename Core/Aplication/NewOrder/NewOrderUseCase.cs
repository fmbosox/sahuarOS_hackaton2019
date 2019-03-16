using System;
using Core.Domain.Order;
using Core.Model;

namespace Core.Aplication.NewOrder
{
    public class NewOrderUseCase
    {
        private readonly NewOrderResponse _response;
        private readonly SahuarOSContext _context;
        private readonly NewOrderEventDistpacher _eventDistpacher;

        public NewOrderUseCase(SahuarOSContext context, NewOrderEventDistpacher eventDistpacher)
        {
            _context = context;
            _eventDistpacher = eventDistpacher;
            _response = new NewOrderResponse();
        }

        public NewOrderResponse CreateOrder(NewOrderRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);
            var order = Order.Create(customer, request.Now);

            foreach (var orderProductRequest in request.Products)
            {
                var product = _context.Products.Find(orderProductRequest.ProductId);
                var orderProduct = OrderProduct.Create(product, request.Now);
                orderProduct.IncreaseAmount(orderProductRequest.Amount);
                order.AddProduct(orderProduct);
            }

            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                _eventDistpacher.Distpach(order);
                _response.Exitosa = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return _response;
        }
    }
}
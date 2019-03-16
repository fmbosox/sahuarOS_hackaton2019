using System;
using Core.Domain.Order;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Core.Aplication.StartProduction.StarProduct
{
    public class StartProductoUseCase
    {
        private readonly SahuarOSContext _context;

        public StartProductoUseCase(SahuarOSContext context)
        {
            _context = context;
        }

        public StartProductoResponse Start(StartProductoRequest request)
        {
            var order = _context.Orders.Find(request.OrderId);
            var gCode = order.StarProduct(request.ProductId, request.Now);
            try
            {
                _context.Entry(order).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new StartProductoResponse();
            }

            return new StartProductoResponse(){GCode = gCode};
        }
    }
}
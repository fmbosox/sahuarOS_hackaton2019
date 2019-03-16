using System;
using System.Collections.Generic;

namespace Core.Aplication.NewOrder
{
    public class NewOrderRequest
    {
        public int CustomerId { get; set; }
        public ICollection<OrderProductRequest> Products { get; set; }
        public DateTime Now;
    }
}
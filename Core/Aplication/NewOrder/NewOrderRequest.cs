using System;
using System.Collections.Generic;

namespace Core.Aplication.NewOrder
{
    public class NewOrderRequest
    {
        public Guid UserId { get; set; }
        public ICollection<int> Products { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Core.SharedKernel;

namespace Core.Domain.Order
{
    public class Order
    {
        public int Id { get; protected set; }
        public virtual Customer Customer { get; protected set; }
        public virtual ICollection<OrderProduct> Products { get; protected set; }
        
        protected Order(){}

        public void AddProduct(OrderProduct product)
        {
            if (Products == null) Products = new List<OrderProduct>();
            var orderProduct = Products.ToList().Find(p => p.Product.Id == product.Product.Id);

            if (orderProduct != null)
                orderProduct.IncreaseAmount(product.Amount) ;
            else
                Products.Add(product);
        }
    }
}
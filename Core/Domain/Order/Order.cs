using System;
using System.Collections.Generic;
using System.Linq;
using Core.SharedKernel;

namespace Core.Domain.Order
{
    public class Order
    {
        public enum OrderStatus
        {
            Received,
            Started,
            Finished,
        }

        public int Id { get; protected set; }
        public virtual Customer Customer { get; protected set; }
        public virtual ICollection<OrderProduct> Products { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime LastModified { get; protected set; }
        public OrderStatus Status { get; protected set; }


        protected Order()
        {
        }

        /*public Order(Customer customer, DateTime createdAt)
        {
            Customer = customer;
            CreatedAt = createdAt;
            LastModified = createdAt;
            Status = OrderStatus.Received;
        }*/


        public void AddProduct(OrderProduct product)
        {
            if (Products == null) Products = new List<OrderProduct>();
            var orderProduct = Products.ToList().Find(p => p.Product.Id == product.Product.Id);

            if (orderProduct != null)
                orderProduct.IncreaseAmount(product.Amount);
            else
                Products.Add(product);
        }

        public static Order Create(Customer customer, DateTime createdAt)
        {
            return new Order {Customer = customer, CreatedAt = createdAt, LastModified = createdAt};
        }
    }
}
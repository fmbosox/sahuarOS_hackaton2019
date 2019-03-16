using System;

namespace Core.Domain.Order
{
    public class OrderProduct
    {
        public enum OrderProductStatus
        {
            Received,
            Started,
            Finished,
        }


        public int Id { get; protected set; }
        public int Amount { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime LastModified { get; protected set; }
        public OrderProductStatus Status { get; protected set; }
        public virtual Product Product { get; protected set; }

        protected OrderProduct()
        {
        }

        /*public OrderProduct(Product product, DateTime createdAt)
        {
            CreatedAt = createdAt;
            LastModified = createdAt;
            Status = OrderProductStatus.Received;
            Product = product;
        }*/

        public int IncreaseAmount(int amount)
        {
            return Amount += amount;
        }


        public static OrderProduct Create(Product product, DateTime createdAt)
        {
            return new OrderProduct {Product = product, CreatedAt = createdAt, LastModified = createdAt};
        }
    }
}
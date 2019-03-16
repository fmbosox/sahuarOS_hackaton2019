using System;

namespace Core.Domain.Order
{
    public class OrderProduct
    {
        public int Id { get; protected set; }
        public int Amount { get; protected set; }
        public virtual Product Product { get; protected set; }


        public int IncreaseAmount(int amount)
        {
            return Amount += amount;
        }
    }
}
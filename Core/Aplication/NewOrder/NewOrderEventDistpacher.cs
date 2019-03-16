using Core.Domain.Order;

namespace Core.Aplication.NewOrder
{
    public interface NewOrderEventDistpacher
    {
         void Distpach(Order order);
    }
}
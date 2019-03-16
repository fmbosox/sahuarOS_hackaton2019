namespace Core.Aplication.NewOrder
{
    public class NewOrderUseCase
    {
        private readonly NewOrderResponse _response;

        public NewOrderUseCase()
        {
            _response = new NewOrderResponse();
        }

        public NewOrderResponse CreateOrder(NewOrderRequest request)
        {
            return _response;
        }
    }
}
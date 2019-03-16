using Core.Aplication.NewOrder;
using Microsoft.AspNetCore.Mvc;

namespace SahuarOS.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Create(NewOrderRequest request)
        {
            var useCase = new NewOrderUseCase();
            var response = useCase.CreateOrder(request);
            
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
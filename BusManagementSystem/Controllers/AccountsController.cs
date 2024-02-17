using Microsoft.AspNetCore.Mvc;

namespace BusManagementSystem.Controllers
{
   
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

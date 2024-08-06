using Microsoft.AspNetCore.Mvc;

namespace CleanArcMvc.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

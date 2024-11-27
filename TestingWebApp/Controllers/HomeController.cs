using Microsoft.AspNetCore.Mvc;

namespace TestingWebApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/GetGreetingsString")]
        public string GetGreetingsString()
        {
            return "It's working";
        }
    }
}

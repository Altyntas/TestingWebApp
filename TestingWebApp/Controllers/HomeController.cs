using Microsoft.AspNetCore.Mvc;
using TestingWebApp.ViewModel;

namespace TestingWebApp.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : Controller
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetGreetingsString")]
        public string GetGreetingsString()
        {
            return "It's working";
        }

        [HttpPost]
        [Route("UploadFile")]
        public bool UploadFile(FileVM file)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                //logger log error
                return false;
            }
        }
    }
}

using DAL;
using DAL.Models;
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

        [HttpGet]
        [Route("GetAllFiles")]
        public List<DataFile> GetAllFiles()
        {
            var context = new WebAppContext();
            var dataFiles = context.DataFiles.ToList();

            return dataFiles;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<bool> UploadFile([FromForm] FileVM file)
        {
            try
            {
                if (file is null || file.File is null)
                    return false;

                byte[] dataFile;
                using (var ms = new MemoryStream())
                {
                    await file.File.CopyToAsync(ms);
                    dataFile = ms.ToArray();
                }

                var context = new WebAppContext();
                await context.DataFiles.AddAsync(new DataFile()
                {
                    Id = Guid.NewGuid(),
                    Name = file?.Name ?? string.Empty,
                    Description = file?.Description ?? string.Empty,
                    ContentType = file?.File?.ContentType,
                    UploadDate = DateTime.Now,
                    Data = dataFile
                });
                await context.SaveChangesAsync();

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

using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TestingWebApp.ViewModel;

namespace TestingWebApp.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : Controller
    {
        public HomeController()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; //TODO: move in appsettings
        }

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
                var dFile = await context.DataFiles.AddAsync(new DataFile()
                {
                    Id = Guid.NewGuid(),
                    Name = file?.Name ?? string.Empty,
                    Description = file?.Description ?? string.Empty,
                    ContentType = file?.File?.ContentType,
                    UploadDate = DateTime.Now,
                    Data = dataFile
                });

                var columns = GetColumns(dataFile, dFile.Entity.Id);

                await context.DataFileColumns.AddRangeAsync(columns);

                await context.Queues.AddAsync(new Queue()
                {
                    Id = new Guid(),
                    EntityId = dFile.Entity.Id,
                    DateCreate = DateTime.Now,
                    QueueType = 0 //file
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

        private List<DataFileColumns> GetColumns(byte[] file, Guid dataFileId)
        {
            if (file is null)
                return new List<DataFileColumns>();

            using (var memStream = new MemoryStream(file))
            {
                var excelPackage = new ExcelPackage(memStream);

                var sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                var columns = new List<DataFileColumns>();
                for (int i = 1; i < sheet.Columns.Count() + 1; i++)
                {
                    columns.Add(new DataFileColumns()
                    {
                        Name = sheet.Cells[1, i].Text,
                        ColumnTypeId = 1,
                        DataFileId = dataFileId
                    });
                }

                return columns;
            }
        }
    }
}

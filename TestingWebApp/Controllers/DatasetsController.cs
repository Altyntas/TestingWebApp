using DAL;
using Microsoft.AspNetCore.Mvc;
using TestingWebApp.ViewModel;

namespace TestingWebApp.Controllers
{
    [ApiController]
    [Route("api/datasets")]
    public class DatasetsController : Controller
    {
        public DatasetsController() 
        { 
        
        }

        [HttpGet]
        [Route("GetAllDatasets")]
        public List<DatasetsVM> GetAllDatasets()
        {
            var context = new WebAppContext();

            return context.DataSetTables.Select(d => new DatasetsVM
            {
                Id = d.Id.ToString(),
                Name = d.Name,
            }).ToList();
        }
    }
}

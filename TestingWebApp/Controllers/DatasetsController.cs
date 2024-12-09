using Common;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Linq.Dynamic.Core;
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

        private class SpecificModel()
        {
            private string[]? DateAndTime { get; set; }
            private string[]? NameOfDimension { get; set; }
            private string[]? ValueOfDimension { get; set; }
        }


        [HttpGet]
        [Route("GetDatasetChart")]
        public void GetDatasetChart()
        {
            var context = new WebAppContext();

            var tableName = context.DataSetTables.Where(d => d.Id == Guid.Parse("f340601a-11f1-4f5e-b0a9-54811f04a249")).FirstOrDefault().Name;

            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = $"SELECT * FROM dbo.{tableName}";
                context.Database.OpenConnection();
                var reader = cmd.ExecuteReader();

                var columns = Enumerable.Range(0, reader.FieldCount)
                        .Select(reader.GetName)
                        .ToList();

                var testest = reader.GetValue(reader.GetOrdinal(columns[0]));

                var testData = reader.ToDynamicList();
                var testReader = testData[0];
            }
        }
    }
}

using Common;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Dynamic;
using System.Linq.Dynamic.Core;
using TestingWebApp.ViewModel;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;


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


        /// <summary>
        /// Get all data from dynamic table
        /// </summary>
        ///
        [HttpGet]
        [Route("GetDatasetChart")]
        public List<DatasetColumnValueVM> GetDatasetChart()
        {
            var context = new WebAppContext();

            var tableName = "soapdata_2025_01_12";// context.DataSetTables.Where(d => d.Id == Guid.Parse("f340601a-11f1-4f5e-b0a9-54811f04a249")).FirstOrDefault().Name;

            using (var cmd = context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = $"SELECT * FROM dbo.{tableName}";
                context.Database.OpenConnection();
                var reader = cmd.ExecuteReader();
                var columns = Enumerable.Range(0, reader.FieldCount)
                        .Select(reader.GetName)
                        .ToList();

                var datasetColumnValue = new List<DatasetColumnValueVM>();
                while (reader.Read())
                {
                    var dataList = new List<ColumnValueVM>();
                    foreach (var column in columns)
                    {
                        var data = reader.GetValue(reader.GetOrdinal(column));
                        dataList.Add(new ColumnValueVM
                        {
                            Name = column,
                            Value = data
                        });
                    }
                    var dateDim = dataList.Where(d => d.Name.Contains("date")).Select(d => d.Value).FirstOrDefault()?.ToString();
                    DateTime.TryParse(dateDim, out var date);
                    datasetColumnValue.Add(new DatasetColumnValueVM()
                    {
                        ColumnValueList = dataList,
                        DateDimension = date.ToString("yyyy-MM-dd")
                    });
                }

                
                return datasetColumnValue;
            }
        }
    }
}

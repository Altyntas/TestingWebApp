using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorker.DataMapping
{
    public class DataMap
    {
        private WebAppContext _context;
        public DataMap(WebAppContext context) 
        { 
            _context = context;
        }

        public async Task<DataSetTable> MapExcelFileAsync(DAL.Models.Queue queue)
        {
            if (queue == null) throw new ArgumentNullException(nameof(queue));

            var excelFile = await _context.DataFiles.Where(d => d.Id == queue.EntityId).FirstOrDefaultAsync();

            if (excelFile == null) throw new ArgumentNullException(nameof(excelFile));

            if (excelFile?.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                throw new Exception("Wrong file type");

            var excelDataByteArray = excelFile?.Data;

            using (var memStream = new MemoryStream(excelDataByteArray))
            {
                var excelPackage = new ExcelPackage(memStream);

                var sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                var columns = new List<DataSetColumns>();
                for(int i = 1; i < sheet.Columns.Count() + 1; i++)
                {
                    columns.Add(new DataSetColumns()
                    {
                        Name = sheet.Cells[1, i].Text,
                        Type = "string" //TODO: Need to develop types parcing
                    });
                }

                var tableName = $"{excelFile.Name}_{DateTime.Now}";
                var table = new DataSetTable()
                {
                    Name = $"{excelFile.Name}_{DateTime.Now.ToString("yyyy_MM_dd")}",
                    Columns = columns,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                return table;
            }
        }
    }
}

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
                        Type = "VARCHAR(50)" //TODO: Need to create type parcing
                    });
                }

                var tableName = excelFile.Name.Split('.')[0];
                //TODO: create scheme for users datasets
                var table = new DataSetTable()
                {
                    Name = $"{tableName}_{DateTime.Now.ToString("yyyy_MM_dd")}",
                    Columns = columns,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                //Creating table with columns and data type
                //await _context.Database.ExecuteSqlRawAsync($"CREATE TABLE dbo.{table.Name} ");
                var createTableSqlString = $"CREATE TABLE dbo.{table.Name} (";
                for (int i = 0; i < table.Columns.Count(); i++)
                {
                    var col = table.Columns.ToList()[i]; //SIC!
                    createTableSqlString += $"{col.Name} {col.Type}";

                    if (i < table.Columns.Count() - 1)
                        createTableSqlString += ", ";
                } 
                
                createTableSqlString += ");";
                await _context.Database.ExecuteSqlRawAsync(createTableSqlString);


                for (int j = 1; j < sheet.Rows.Count() + 1; j++)
                {
                    if (j == 1) continue; //ignore first row

                    var rowValues = new List<string>();

                    for (int i = 1; i < sheet.Columns.Count() + 1; i++)
                        rowValues.Add(sheet.Cells[2, i].Text);

                    var insertString = $"INSERT INTO dbo.{table.Name}({string.Join(',', table.Columns.Select(c => c.Name))}) VALUES({string.Join(',', rowValues.Select(r => "\'" + r + "\'"))})";
                    await _context.Database.ExecuteSqlRawAsync(insertString);
                }
                

                return table;
            }
        }
    }
}

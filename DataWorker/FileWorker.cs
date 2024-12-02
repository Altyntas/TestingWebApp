using DAL;
using DataWorker.DataMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorker
{
    public class FileWorker : BackgroundService
    {
        private readonly IDbContextFactory<WebAppContext> _dbContext;
        public FileWorker(IDbContextFactory<WebAppContext> dbContext)
        {
            _dbContext = dbContext;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; //TODO: move in appsettings
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessFiles();
                    await Task.Delay(10000);
                }
                catch (Exception ex)
                {
                    //add logger
                    await Task.Delay(15000);
                }
            }
        }

        private async Task ProcessFiles()
        {
            var context = _dbContext.CreateDbContext();

            var queuesToComplete = await context.Queues.Where(q => !q.IsCompleted && q.QueueType == 0).ToListAsync();

            var dataMap = new DataMap(context);

            foreach (var queue in queuesToComplete)
            {
                var dataset = await dataMap.MapExcelFileAsync(queue);
                context.DataSetTables.Add(dataset);
                queue.IsCompleted = true;
                queue.DateUpdate = DateTime.Now;
                queue.DatasetTableGuid = dataset.Id;

                await context.SaveChangesAsync();
            }
        }
    }
}

using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await JustCheckingReally();
                    await Task.Delay(10000);
                }
                catch (Exception ex)
                {
                    //add logger
                    await Task.Delay(15000);
                }
            }
        }

        private async Task JustCheckingReally()
        {
            var context = _dbContext.CreateDbContext();

            var filesToComplete = context.Queues.Where(q => !q.IsCompleted && q.QueueType == 0).ToList();

            foreach ( var file in filesToComplete )
            {
                file.IsCompleted = true;
                file.DateUpdate = DateTime.Now;
            }

            await context.SaveChangesAsync();
        }
    }
}

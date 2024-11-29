using DAL;
using DataWorker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureLogging((logging) => //TODO: add logging
                    {

                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddDbContextFactory<WebAppContext>();
                        services.AddHostedService<FileWorker>();
                    });
    }

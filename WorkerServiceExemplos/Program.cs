using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerServiceExemplos
{
    //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio
    //https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/multi-container-microservice-net-applications/background-tasks-with-ihostedservice
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<StartWorker>();
                    services.AddHostedService<LoopWorker>();
                    services.AddHostedService<HelloWorldWoker>();
                });
    }
}

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreExemplos.Services
{
    //IHostedService é iniciado quando a Api é executada e executa o StartAsync de acordo com o timer definido.
    //Caso não seja utilizado um timer, o procedimento será executado apenas quando a Api subir.
    //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio
    public class StartApiHostedServices : IHostedService
    {
        private readonly ILogger<StartApiHostedServices> _logger;

        public StartApiHostedServices(ILogger<StartApiHostedServices> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartApi();
            return Task.CompletedTask;
        }

        public void StartApi()
        {
            _logger.LogInformation("Api sendo inicializada...");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

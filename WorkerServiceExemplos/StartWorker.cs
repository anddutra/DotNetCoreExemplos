using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceExemplos
{
    public class StartWorker : IHostedService
    {
        private readonly ILogger<StartWorker> _logger;

        public StartWorker(ILogger<StartWorker> logger)
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

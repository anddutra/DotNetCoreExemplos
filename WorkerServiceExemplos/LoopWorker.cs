using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceExemplos
{
    public class LoopWorker : BackgroundService
    {
        private readonly ILogger<LoopWorker> _logger;

        public LoopWorker(ILogger<LoopWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Loop Worker!");
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}

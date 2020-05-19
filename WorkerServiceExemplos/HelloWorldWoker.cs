using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceExemplos
{
    public class HelloWorldWoker : IHostedService, IDisposable
    {
        private readonly ILogger<HelloWorldWoker> _logger;
        private System.Timers.Timer _timer;

        public HelloWorldWoker(ILogger<HelloWorldWoker> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new System.Timers.Timer(TimeSpan.FromSeconds(2).TotalMilliseconds);
            _timer.Elapsed += HelloWorld;
            _timer.Start();
            return Task.CompletedTask;
        }

        public void HelloWorld(object sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogInformation("Hello World!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

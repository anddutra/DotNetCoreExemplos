using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreExemplos.Services
{
    //IHostedService é iniciado quando a Api é executada e executa o StartAsync de acordo com o timer definido.
    //Caso não seja utilizado um timer, o procedimento será executado apenas quando a Api subir.
    //A cada 10 segundos será impresso Hello World no console.
    //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio
    public class HelloWorldHostedServices : IHostedService, IDisposable
    {
        private readonly ILogger<HelloWorldHostedServices> _logger;
        private System.Timers.Timer _timer;

        public HelloWorldHostedServices(ILogger<HelloWorldHostedServices> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new System.Timers.Timer(10000);
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

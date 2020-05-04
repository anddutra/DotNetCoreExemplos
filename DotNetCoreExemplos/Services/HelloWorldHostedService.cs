using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreExemplos.Services
{
    //IHostedService é iniciado quando a Api é executada e executa o StartAsync de acordo com o timer definido.
    //Caso não seja utilizado um timer, o procedimento será executado apenas quando a Api subir.
    //A cada 10 segundos será impresso Hello World no console.
    //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio
    public class HelloWorldHostedService : IHostedService
    {
        private readonly ILogger<HelloWorldHostedService> _logger;
        private System.Timers.Timer _timer;

        public HelloWorldHostedService(ILogger<HelloWorldHostedService> logger)
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
            _timer.Dispose();
            return Task.CompletedTask;
        }
    }
}

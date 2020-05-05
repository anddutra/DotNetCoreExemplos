using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreExemplos.Services
{
    //IHostedService é iniciado quando a Api é executada e executa o StartAsync de acordo com o timer definido.
    //Caso não seja utilizado um timer, o procedimento será executado apenas quando a Api subir.
    //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio
    public class StartApiHostedService : IHostedService
    {
        private readonly ILogger<StartApiHostedService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public StartApiHostedService(ILogger<StartApiHostedService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartApi();
            GetRestApi();
            return Task.CompletedTask;
        }

        public void StartApi()
        {
            _logger.LogInformation("Api sendo inicializada...");
        }


        //Realiza a criação do httpClient com o nome HttpClientApi, conforme criado no Startup
        //Faz a chamada para a API worldtimeapi passando na url America/Sao_Paulo
        //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1
        //http://worldtimeapi.org/
        public async void GetRestApi()
        {
            var client = _httpClientFactory.CreateClient("HttpClientApi");
            var request = new HttpRequestMessage(HttpMethod.Get, "America/Sao_Paulo");

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Api worldtimeapi chamada com sucesso.");
            }
            else
            {
                _logger.LogInformation("Falha ao chamar a api worldtimeapi.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

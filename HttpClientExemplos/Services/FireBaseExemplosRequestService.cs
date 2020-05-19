using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExemplos.Services
{
    public class FireBaseExemplosRequestService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public FireBaseExemplosRequestService(ILogger<WorldTimeApiRequestService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> GetApiFireBaseExemplos()
        {
            var client = CreateHttpClientFromNewScope();
            var request = new HttpRequestMessage(HttpMethod.Get, "GetUsers");

            try
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Api worldtimeapi chamada com sucesso.");
                    return jsonResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao chamar a api FireBaseExemplos.");
            }

            return "Erro ao chamar a api FireBaseExemplos";
        }

        //Criado novo escopo para obter o IHttpClientFactory
        //Isso é feito para que não seja necessário injetar ele na classe
        //Quando injetamos uma classe Scoped/Transient em uma Singleton ela virá Singleton
        private HttpClient CreateHttpClientFromNewScope()
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var httpClientFactory = scope.ServiceProvider.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient("FireBaseExemplos");
            return client;
        }
    }
}

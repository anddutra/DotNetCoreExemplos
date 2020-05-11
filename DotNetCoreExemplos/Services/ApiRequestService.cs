using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetCoreExemplos.Services
{
    public class ApiRequestService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public ApiRequestService(ILogger<ApiRequestService> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        //Realiza a criação do httpClient com o nome HttpClientApi, conforme criado no Startup
        //Faz a chamada para a Api worldtimeapi passando na url America/Sao_Paulo
        //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1
        //http://worldtimeapi.org/
        public async Task<string> GetApiWorldTime()
        {
            var client = _httpClientFactory.CreateClient("HttpClientApi");
            var request = new HttpRequestMessage(HttpMethod.Get, "America/Sao_Paulo");

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Api worldtimeapi chamada com sucesso.");
                return jsonResponse;
            }
            else
            {
                _logger.LogInformation("Falha ao chamar a api worldtimeapi.");
                return "Erro ao chamar a api worldtimeapi";
            }
        }
    }
}

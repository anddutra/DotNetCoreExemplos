using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExemplos.Services
{
    public class WorldTimeApiRequestService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public WorldTimeApiRequestService(ILogger<WorldTimeApiRequestService> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<string> GetApiWorldTime()
        {
            var client = _httpClientFactory.CreateClient("WorldTime");
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

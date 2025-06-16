using System.Net.Http;
using Client.Services.Handlers;

namespace Client.Services
{
    public partial class ApiClientService
    {
        private static readonly HttpClient httpClient;

        // Базовый адрес API. Поставить порт сервера
        private const string ApiBaseUrl = "https://localhost:57943";

        public bool UseEntityFramework { get; set; } = true;

        static ApiClientService()
        {
            var strategyHandler = new StrategyHandler
            {
                InnerHandler = new HttpClientHandler()
            };

            httpClient = new HttpClient(strategyHandler)
            {
                BaseAddress = new System.Uri(ApiBaseUrl)
            };
        }

        public ApiClientService()
        {
            StrategyHandler.ApiClientServiceInstance = this;
        }
    }
}

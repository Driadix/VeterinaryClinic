using System.Net.Http;
using System.Net.Http.Json;

using ClientModel = Core.Models.Client;

namespace Client.Services
{
    public class ApiClientService
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // Базовый адрес API. Поставить порт сервера
        private const string ApiBaseUrl = "https://localhost:57943";

        public ApiClientService()
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new System.Uri(ApiBaseUrl);
            }
        }

        /// <summary>
        /// Асинхронно получает список всех клиентов с сервера.
        /// </summary>
        /// <returns>Коллекция клиентов или null в случае ошибки.</returns>
        public async Task<IEnumerable<ClientModel>?> GetClientsAsync()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<ClientModel>>("/api/clients");
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка API запроса: {ex.Message}");
                return null;
            }
        }
    }
}

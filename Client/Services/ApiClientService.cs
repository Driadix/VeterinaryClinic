using System.Net.Http;
using System.Net.Http.Json;

using ClientModel = Core.Models.Client;
using PetModel = Core.Models.Pet;

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

        public async Task<IEnumerable<ClientModel>?> GetClientsAsync()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<ClientModel>>("/api/clients");
            }
            catch (HttpRequestException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); return null; }
        }

        public async Task<ClientModel?> AddClientAsync(ClientModel client)
        {
            var response = await httpClient.PostAsJsonAsync("/api/clients", client);
            return await response.Content.ReadFromJsonAsync<ClientModel>();
        }

        public async Task UpdateClientAsync(ClientModel client)
        {
            await httpClient.PutAsJsonAsync($"/api/clients/{client.Id}", client);
        }

        public async Task DeleteClientAsync(int clientId)
        {
            await httpClient.DeleteAsync($"/api/clients/{clientId}");
        }

        public async Task<IEnumerable<PetModel>?> GetPetsForClientAsync(int clientId)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<PetModel>>($"/api/pets/client/{clientId}");
            }
            catch (HttpRequestException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); return null; }
        }

        public async Task<PetModel?> AddPetAsync(PetModel pet)
        {
            var response = await httpClient.PostAsJsonAsync("/api/pets", pet);
            return await response.Content.ReadFromJsonAsync<PetModel>();
        }

        public async Task UpdatePetAsync(PetModel pet)
        {
            await httpClient.PutAsJsonAsync($"/api/pets/{pet.Id}", pet);
        }

        public async Task DeletePetAsync(int petId)
        {
            await httpClient.DeleteAsync($"/api/pets/{petId}");
        }
    }
}

using System.Net.Http;
using System.Net.Http.Json;

using ClientModel = Core.Models.Client;
using PetModel = Core.Models.Pet;
using AppointmentModel = Core.Models.Appointment;
using UserModel = Core.Models.User;

namespace Client.Services
{
    public class ApiClientService
    {
        private static readonly HttpClient httpClient = new HttpClient();

        // Базовый адрес API. Поставить порт сервера
        private const string ApiBaseUrl = "https://localhost:57943";

        public bool UseEntityFramework { get; set; } = true;

        public ApiClientService()
        {
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new System.Uri(ApiBaseUrl);
            }
        }

        private void SetRequestHeader()
        {
            httpClient.DefaultRequestHeaders.Remove("X-Data-Access-Strategy");
            var strategyValue = UseEntityFramework ? "EFCore" : "RawSQL";
            httpClient.DefaultRequestHeaders.Add("X-Data-Access-Strategy", strategyValue);
        }

        public async Task<IEnumerable<ClientModel>?> GetClientsAsync()
        {
            SetRequestHeader();
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<ClientModel>>("/api/clients");
            }
            catch (HttpRequestException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); return null; }
        }

        public async Task<ClientModel?> AddClientAsync(ClientModel client)
        {
            SetRequestHeader();
            var response = await httpClient.PostAsJsonAsync("/api/clients", client);
            return await response.Content.ReadFromJsonAsync<ClientModel>();
        }

        public async Task UpdateClientAsync(ClientModel client)
        {
            SetRequestHeader();
            await httpClient.PutAsJsonAsync($"/api/clients/{client.Id}", client);
        }

        public async Task DeleteClientAsync(int clientId)
        {
            SetRequestHeader();
            await httpClient.DeleteAsync($"/api/clients/{clientId}");
        }

        public async Task<IEnumerable<PetModel>?> GetAllPetsAsync()
        {
            SetRequestHeader();
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<PetModel>>("/api/pets");
            }
            catch (HttpRequestException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); return null; }
        }

        public async Task<IEnumerable<PetModel>?> GetPetsForClientAsync(int clientId)
        {
            SetRequestHeader();
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<PetModel>>($"/api/pets/client/{clientId}");
            }
            catch (HttpRequestException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); return null; }
        }

        public async Task<PetModel?> AddPetAsync(PetModel pet)
        {
            SetRequestHeader();
            var response = await httpClient.PostAsJsonAsync("/api/pets", pet);
            return await response.Content.ReadFromJsonAsync<PetModel>();
        }

        public async Task UpdatePetAsync(PetModel pet)
        {
            SetRequestHeader();
            await httpClient.PutAsJsonAsync($"/api/pets/{pet.Id}", pet);
        }

        public async Task DeletePetAsync(int petId)
        {
            SetRequestHeader();
            await httpClient.DeleteAsync($"/api/pets/{petId}");
        }

        public async Task<IEnumerable<AppointmentModel>?> GetAppointmentsAsync()
        {
            SetRequestHeader();
            try { return await httpClient.GetFromJsonAsync<IEnumerable<AppointmentModel>>("/api/appointments"); }
            catch (HttpRequestException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); return null; }
        }

        public async Task<AppointmentModel?> AddAppointmentAsync(AppointmentModel appointment)
        {
            SetRequestHeader();
            var response = await httpClient.PostAsJsonAsync("/api/appointments", appointment);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AppointmentModel>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"API Error: {errorContent}");
            return null;
        }

        public async Task UpdateAppointmentAsync(AppointmentModel appointment)
        {
            SetRequestHeader();
            await httpClient.PutAsJsonAsync($"/api/appointments/{appointment.Id}", appointment);
        }

        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            SetRequestHeader();
            await httpClient.DeleteAsync($"/api/appointments/{appointmentId}");
        }

        public async Task<IEnumerable<UserModel>?> GetUsersAsync()
        {
            SetRequestHeader();
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<UserModel>>("/api/users");
            }
            catch (HttpRequestException ex) { System.Diagnostics.Debug.WriteLine(ex.Message); return null; }
        }
    }
}

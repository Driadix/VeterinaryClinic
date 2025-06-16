using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClientModel = Core.Models.Client;

namespace Client.Services;

public partial class ApiClientService
{
    #region Client Methods
    public async Task<IEnumerable<ClientModel>?> GetClientsAsync()
    {
        try { return await httpClient.GetFromJsonAsync<IEnumerable<ClientModel>>("/api/clients"); }
        catch { return null; }
    }

    public async Task<ClientModel?> AddClientAsync(ClientModel client)
    {
        var response = await httpClient.PostAsJsonAsync("/api/clients", client);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<ClientModel>();
        return null;
    }

    public async Task UpdateClientAsync(ClientModel client)
    {
        await httpClient.PutAsJsonAsync($"/api/clients/{client.Id}", client);
    }

    public async Task DeleteClientAsync(int clientId)
    {
        await httpClient.DeleteAsync($"/api/clients/{clientId}");
    }
    #endregion
}
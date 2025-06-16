using System.Net.Http.Json;
using PetModel = Core.Models.Pet;

namespace Client.Services;

public partial class ApiClientService
{
    #region Pet Methods
    public async Task<IEnumerable<PetModel>?> GetAllPetsAsync()
    {
        try { return await httpClient.GetFromJsonAsync<IEnumerable<PetModel>>("/api/pets"); }
        catch { return null; }
    }

    public async Task<IEnumerable<PetModel>?> GetPetsForClientAsync(int clientId)
    {
        try { return await httpClient.GetFromJsonAsync<IEnumerable<PetModel>>($"/api/pets/client/{clientId}"); }
        catch { return null; }
    }

    public async Task<PetModel?> AddPetAsync(PetModel pet)
    {
        var response = await httpClient.PostAsJsonAsync("/api/pets", pet);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<PetModel>();
        return null;
    }

    public async Task UpdatePetAsync(PetModel pet)
    {
        await httpClient.PutAsJsonAsync($"/api/pets/{pet.Id}", pet);
    }

    public async Task DeletePetAsync(int petId)
    {
        await httpClient.DeleteAsync($"/api/pets/{petId}");
    }
    #endregion
}
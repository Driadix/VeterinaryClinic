using System.Net.Http.Json;
using UserModel = Core.Models.User;

namespace Client.Services;

public record AuthRequest(string Username, string Password);

public partial class ApiClientService
{
    #region Auth Methods

    public async Task<UserModel?> RegisterAsync(string username, string password)
    {
        var request = new AuthRequest(username, password);
        var response = await httpClient.PostAsJsonAsync("/api/auth/register", request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserModel>();
        }
        return null;
    }

    public async Task<UserModel?> LoginAsync(string username, string password)
    {
        var request = new AuthRequest(username, password);
        var response = await httpClient.PostAsJsonAsync("/api/auth/login", request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserModel>();
        }
        return null;
    }

    #endregion
}
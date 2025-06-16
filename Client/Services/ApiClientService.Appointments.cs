using System.Net.Http.Json;
using AppointmentModel = Core.Models.Appointment;
using UserModel = Core.Models.User;

namespace Client.Services;

public partial class ApiClientService
{
    #region Appointment & User Methods
    public async Task<IEnumerable<AppointmentModel>?> GetAppointmentsAsync()
    {
        try { return await httpClient.GetFromJsonAsync<IEnumerable<AppointmentModel>>("/api/appointments"); }
        catch { return null; }
    }

    public async Task<AppointmentModel?> AddAppointmentAsync(AppointmentModel appointment)
    {
        var response = await httpClient.PostAsJsonAsync("/api/appointments", appointment);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<AppointmentModel>();
        return null;
    }

    public async Task UpdateAppointmentAsync(AppointmentModel appointment)
    {
        await httpClient.PutAsJsonAsync($"/api/appointments/{appointment.Id}", appointment);
    }

    public async Task DeleteAppointmentAsync(int appointmentId)
    {
        await httpClient.DeleteAsync($"/api/appointments/{appointmentId}");
    }

    public async Task<IEnumerable<UserModel>?> GetUsersAsync()
    {
        try { return await httpClient.GetFromJsonAsync<IEnumerable<UserModel>>("/api/users"); }
        catch { return null; }
    }
    #endregion
}
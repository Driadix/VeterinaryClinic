using Core.Models;

namespace BusinessLogic.Services;

public partial class ClinicService
{
    #region Appointment Management

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _strategy.GetAppointmentRepository().GetAllAsync();
    }

    public async Task AddAppointmentAsync(Appointment newAppointment)
    {
        await _strategy.GetAppointmentRepository().AddAsync(newAppointment);
        await _strategy.SaveChangesAsync();
    }

    public async Task<bool> UpdateAppointmentAsync(Appointment appointmentToUpdate)
    {
        var appointmentRepository = _strategy.GetAppointmentRepository();
        var existingAppointment = await appointmentRepository.GetByIdAsync(appointmentToUpdate.Id);

        if (existingAppointment == null) return false;

        existingAppointment.AppointmentDateTime = appointmentToUpdate.AppointmentDateTime;
        existingAppointment.Reason = appointmentToUpdate.Reason;
        existingAppointment.PetId = appointmentToUpdate.PetId;
        existingAppointment.UserId = appointmentToUpdate.UserId;

        await _strategy.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAppointmentAsync(int appointmentId)
    {
        var appointmentRepository = _strategy.GetAppointmentRepository();
        var appointmentToDelete = await appointmentRepository.GetByIdAsync(appointmentId);

        if (appointmentToDelete == null) return false;

        appointmentRepository.Delete(appointmentToDelete);
        await _strategy.SaveChangesAsync();
        return true;
    }

    #endregion
}
using Core.Models;

namespace BusinessLogic.Services;

public partial class ClinicService
{
    #region Client Management

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _strategy.GetClientRepository().GetAllAsync();
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _strategy.GetClientRepository().GetByIdAsync(id);
    }

    public async Task AddClientAsync(Client newClient)
    {
        await _strategy.GetClientRepository().AddAsync(newClient);
        await _strategy.SaveChangesAsync();
    }

    public async Task<bool> UpdateClientAsync(Client clientToUpdate)
    {
        var clientRepository = _strategy.GetClientRepository();
        var existingClient = await clientRepository.GetByIdAsync(clientToUpdate.Id);

        if (existingClient == null) return false;

        existingClient.FullName = clientToUpdate.FullName;
        existingClient.PhoneNumber = clientToUpdate.PhoneNumber;
        existingClient.Email = clientToUpdate.Email;

        await _strategy.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var clientRepository = _strategy.GetClientRepository();
        var clientToDelete = await clientRepository.GetByIdAsync(id);

        if (clientToDelete == null) return false;

        clientRepository.Delete(clientToDelete);
        await _strategy.SaveChangesAsync();
        return true;
    }

    #endregion
}
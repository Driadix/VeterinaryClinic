using Core.Interfaces;
using Core.Models;

namespace BusinessLogic.Services
{
    public class ClinicService
    {
        private readonly IDataAccessStrategy _strategy;

        public ClinicService(IDataAccessStrategy strategy)
        {
            _strategy = strategy;
        }

        #region Client Management

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var clientRepository = _strategy.GetClientRepository();
            return await clientRepository.GetAllAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            var clientRepository = _strategy.GetClientRepository();
            return await clientRepository.GetByIdAsync(id);
        }

        public async Task AddClientAsync(Client newClient)
        {
            if (newClient == null)
            {
                throw new ArgumentNullException(nameof(newClient));
            }

            var clientRepository = _strategy.GetClientRepository();
            await clientRepository.AddAsync(newClient);
            await _strategy.SaveChangesAsync();
        }

        public async Task<bool> UpdateClientAsync(Client clientToUpdate)
        {
            var clientRepository = _strategy.GetClientRepository();

            var existingClient = await clientRepository.GetByIdAsync(clientToUpdate.Id);

            if (existingClient == null)
            {
                return false;
            }

            existingClient.FullName = clientToUpdate.FullName;
            existingClient.PhoneNumber = clientToUpdate.PhoneNumber;
            existingClient.Email = clientToUpdate.Email;

            await _strategy.SaveChangesAsync();
            return true;
        }

        public async Task DeleteClientAsync(int id)
        {
            var clientRepository = _strategy.GetClientRepository();
            var clientToDelete = await clientRepository.GetByIdAsync(id);

            if (clientToDelete != null)
            {
                clientRepository.Delete(clientToDelete);
                await _strategy.SaveChangesAsync();
            }
        }

        #endregion
    }
}

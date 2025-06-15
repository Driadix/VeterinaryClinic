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

        /// <summary>
        /// Регистрирует нового клиента в системе.
        /// </summary>
        public async Task RegisterClientAsync(Client newClient)
        {
            if (newClient == null)
            {
                throw new ArgumentNullException(nameof(newClient));
            }

            var clientRepository = _strategy.GetClientRepository();
            await clientRepository.AddAsync(newClient);
            await _strategy.SaveChangesAsync();
        }

        /// <summary>
        /// Получает список всех клиентов.
        /// </summary>
        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var clientRepository = _strategy.GetClientRepository();
            return await clientRepository.GetAllAsync();
        }
    }
}

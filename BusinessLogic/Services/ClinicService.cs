using Core.Interfaces;
using Core.Models;
using System.Linq.Expressions;

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

        #region Pet Management

        public async Task<IEnumerable<Pet>> GetPetsForClientAsync(int clientId)
        {
            var petRepository = _strategy.GetPetRepository();
            return await petRepository.FindAsync(p => p.ClientId == clientId);
        }

        public async Task AddPetAsync(Pet newPet)
        {
            await _strategy.GetPetRepository().AddAsync(newPet);
            await _strategy.SaveChangesAsync();
        }

        public async Task<bool> UpdatePetAsync(Pet petToUpdate)
        {
            var petRepository = _strategy.GetPetRepository();
            var existingPet = await petRepository.GetByIdAsync(petToUpdate.Id);

            if (existingPet == null) return false;

            existingPet.Name = petToUpdate.Name;
            existingPet.Species = petToUpdate.Species;
            existingPet.Breed = petToUpdate.Breed;

            await _strategy.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePetAsync(int petId)
        {
            var petRepository = _strategy.GetPetRepository();
            var petToDelete = await petRepository.GetByIdAsync(petId);

            if (petToDelete != null)
            {
                petRepository.Delete(petToDelete);
                await _strategy.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #endregion
    }
}

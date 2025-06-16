using Core.Models;

namespace BusinessLogic.Services;

public partial class ClinicService
{
    #region Pet Management

    public async Task<IEnumerable<Pet>> GetAllPetsAsync()
    {
        return await _strategy.GetPetRepository().GetAllAsync();
    }

    public async Task<IEnumerable<Pet>> GetPetsForClientAsync(int clientId)
    {
        return await _strategy.GetPetRepository().FindAsync(p => p.ClientId == clientId);
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

        if (petToDelete == null) return false;

        petRepository.Delete(petToDelete);
        await _strategy.SaveChangesAsync();
        return true;
    }

    #endregion
}
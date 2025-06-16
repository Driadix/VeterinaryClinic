using Core.Models;

namespace BusinessLogic.Services;

public partial class ClinicService
{
    #region User Management

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _strategy.GetUserRepository().GetAllAsync();
    }

    #endregion
}
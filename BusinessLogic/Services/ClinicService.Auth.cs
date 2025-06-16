using Core.Models;

namespace BusinessLogic.Services;

public partial class ClinicService
{
    #region Auth Management

    public async Task<User?> RegisterUserAsync(string username, string password)
    {
        var userRepository = _strategy.GetUserRepository();

        var existingUser = (await userRepository.FindAsync(u => u.Username == username)).FirstOrDefault();
        if (existingUser != null)
        {
            return null;
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new User
        {
            Username = username,
            PasswordHash = passwordHash
        };

        await userRepository.AddAsync(newUser);
        await _strategy.SaveChangesAsync();

        return newUser;
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        var userRepository = _strategy.GetUserRepository();
        var user = (await userRepository.FindAsync(u => u.Username == username)).FirstOrDefault();

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }

    #endregion
}
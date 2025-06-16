using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using Core.Interfaces;
using Core.Models;

namespace DataAccess.Repositories.Sql;

public class UserSqlRepository : IRepository<User>
{
    private readonly string _connectionString;

    public UserSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(User entity)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)";
        using var command = new SqlCommand(commandText, connection);

        command.Parameters.AddWithValue("@Username", entity.Username);
        command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
    {
        var allUsers = await GetAllAsync();
        return allUsers.Where(predicate.Compile());
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = new List<User>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT Id, Username, PasswordHash FROM Users";
        using var command = new SqlCommand(commandText, connection);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            users.Add(new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                PasswordHash = reader.GetString(2)
            });
        }
        return users;
    }

    public Task<User?> GetByIdAsync(int id) => throw new NotImplementedException();
    public void Update(User entity) => throw new NotImplementedException();
    public void Delete(User entity) => throw new NotImplementedException();
}
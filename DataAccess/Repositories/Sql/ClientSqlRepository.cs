using Core.Interfaces;
using Core.Models;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace VeterinaryClinic.DataAccess.Repositories.Sql;

public class ClientSqlRepository : IRepository<Client>
{
    private readonly string _connectionString;

    public ClientSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(Client entity)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "INSERT INTO Clients (FullName, PhoneNumber, Email) VALUES (@FullName, @PhoneNumber, @Email)";
        using var command = new SqlCommand(commandText, connection);

        command.Parameters.AddWithValue("@FullName", entity.FullName);
        command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber as object ?? DBNull.Value);
        command.Parameters.AddWithValue("@Email", entity.Email as object ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        var clients = new List<Client>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT Id, FullName, PhoneNumber, Email FROM Clients";
        using var command = new SqlCommand(commandText, connection);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            clients.Add(new Client
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                PhoneNumber = reader.IsDBNull(2) ? null : reader.GetString(2),
                Email = reader.IsDBNull(3) ? null : reader.GetString(3)
            });
        }
        return clients;
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT Id, FullName, PhoneNumber, Email FROM Clients WHERE Id = @Id";
        using var command = new SqlCommand(commandText, connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Client
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                PhoneNumber = reader.IsDBNull(2) ? null : reader.GetString(2),
                Email = reader.IsDBNull(3) ? null : reader.GetString(3)
            };
        }
        return null;
    }

    // Методы ниже оставлены для соответствия интерфейсу, но в данной
    // реализации их полноценное использование затруднено.
    public Task<IEnumerable<Client>> FindAsync(Expression<Func<Client, bool>> predicate)
    {
        return Task.FromResult(Enumerable.Empty<Client>());
    }

    public void Update(Client entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Client entity)
    {
        throw new NotImplementedException();
    }
}
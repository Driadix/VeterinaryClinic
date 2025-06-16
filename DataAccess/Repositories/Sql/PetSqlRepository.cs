using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using Core.Interfaces;
using Core.Models;

namespace DataAccess.Repositories.Sql;

public class PetSqlRepository : IRepository<Pet>
{
    private readonly string _connectionString;

    public PetSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(Pet entity)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "INSERT INTO Pets (Name, Species, Breed, ClientId) VALUES (@Name, @Species, @Breed, @ClientId)";
        using var command = new SqlCommand(commandText, connection);

        command.Parameters.AddWithValue("@Name", entity.Name);
        command.Parameters.AddWithValue("@Species", entity.Species);
        command.Parameters.AddWithValue("@Breed", entity.Breed as object ?? DBNull.Value);
        command.Parameters.AddWithValue("@ClientId", entity.ClientId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Pet>> GetAllAsync()
    {
        var pets = new List<Pet>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT Id, Name, Species, Breed, ClientId FROM Pets";
        using var command = new SqlCommand(commandText, connection);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            pets.Add(new Pet
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Species = reader.GetString(2),
                Breed = reader.IsDBNull(3) ? null : reader.GetString(3),
                ClientId = reader.GetInt32(4)
            });
        }
        return pets;
    }

    public async Task<IEnumerable<Pet>> FindAsync(Expression<Func<Pet, bool>> predicate)
    {
        var allPets = await GetAllAsync();
        return allPets.Where(predicate.Compile());
    }

    public Task<Pet?> GetByIdAsync(int id) => throw new NotImplementedException();
    public void Update(Pet entity) => throw new NotImplementedException();
    public void Delete(Pet entity) => throw new NotImplementedException();
}
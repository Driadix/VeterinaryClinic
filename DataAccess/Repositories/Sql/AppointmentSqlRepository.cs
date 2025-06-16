using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using Core.Interfaces;
using Core.Models;

namespace DataAccess.Repositories.Sql;

public class AppointmentSqlRepository : IRepository<Appointment>
{
    private readonly string _connectionString;

    public AppointmentSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddAsync(Appointment entity)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "INSERT INTO Appointments (AppointmentDateTime, Reason, PetId, UserId) VALUES (@DateTime, @Reason, @PetId, @UserId)";
        using var command = new SqlCommand(commandText, connection);

        command.Parameters.AddWithValue("@DateTime", entity.AppointmentDateTime);
        command.Parameters.AddWithValue("@Reason", entity.Reason as object ?? DBNull.Value);
        command.Parameters.AddWithValue("@PetId", entity.PetId);
        command.Parameters.AddWithValue("@UserId", entity.UserId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        var appointments = new List<Appointment>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var commandText = "SELECT Id, AppointmentDateTime, Reason, PetId, UserId FROM Appointments";
        using var command = new SqlCommand(commandText, connection);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            appointments.Add(new Appointment
            {
                Id = reader.GetInt32(0),
                AppointmentDateTime = reader.GetDateTime(1),
                Reason = reader.IsDBNull(2) ? null : reader.GetString(2),
                PetId = reader.GetInt32(3),
                UserId = reader.GetInt32(4)
            });
        }
        return appointments;
    }

    public Task<Appointment?> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<IEnumerable<Appointment>> FindAsync(Expression<Func<Appointment, bool>> predicate) => throw new NotImplementedException();
    public void Update(Appointment entity) => throw new NotImplementedException();
    public void Delete(Appointment entity) => throw new NotImplementedException();
}
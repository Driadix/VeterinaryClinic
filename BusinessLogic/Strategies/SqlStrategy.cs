using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Configuration;
using VeterinaryClinic.DataAccess.Repositories.Sql;

namespace VeterinaryClinic.BusinessLogic.Strategies;

public class SqlStrategy : IDataAccessStrategy
{
    private readonly string _connectionString;

    public SqlStrategy(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string not found.");
    }

    public IRepository<Client> GetClientRepository()
    {
        return new ClientSqlRepository(_connectionString);
    }

    public IRepository<User> GetUserRepository() => throw new NotImplementedException();
    public IRepository<Pet> GetPetRepository() => throw new NotImplementedException();
    public IRepository<Appointment> GetAppointmentRepository() => throw new NotImplementedException();

    public Task<int> SaveChangesAsync()
    {
        // В подходе с чистым SQL каждая операция не требует общего SaveChanges.
        // Возвращаем 1 для имитации успешного выполнения.
        return Task.FromResult(1);
    }
}
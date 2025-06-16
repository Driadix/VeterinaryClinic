using Core.Interfaces;
using Core.Models;
using DataAccess.Repositories.Sql;
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
    public IRepository<Pet> GetPetRepository()
    {
        return new PetSqlRepository(_connectionString);
    }

    public IRepository<Appointment> GetAppointmentRepository()
    {
        return new AppointmentSqlRepository(_connectionString);
    }
    public IRepository<User> GetUserRepository() => throw new NotImplementedException();

    public Task<int> SaveChangesAsync()
    {
        return Task.FromResult(1);
    }
}
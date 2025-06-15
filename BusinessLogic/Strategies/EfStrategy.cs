using Core.Interfaces;
using Core.Models;
using DataAccess;
using DataAccess.Repositories.EntityFramework;

namespace BusinessLogic.Strategies
{
    public class EfStrategy : IDataAccessStrategy
    {
        private readonly ApplicationDbContext _context;

        private readonly Dictionary<Type, object> _repositories = new();

        public EfStrategy(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<User> GetUserRepository() => GetRepository<User>();
        public IRepository<Client> GetClientRepository() => GetRepository<Client>();
        public IRepository<Pet> GetPetRepository() => GetRepository<Pet>();
        public IRepository<Appointment> GetAppointmentRepository() => GetRepository<Appointment>();

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обобщенный метод для получения репозитория нужного типа.
        /// Берем репозиторий из кеша или кешируем и возвращаем новый
        /// </summary>
        private IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IRepository<T>)repository;
            }

            var newRepository = new EfRepository<T>(_context);
            _repositories[typeof(T)] = newRepository;
            return newRepository;
        }
    }
}

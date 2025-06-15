using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Асинхронно получает сущность по ее идентификатору.
        /// </summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Асинхронно получает все сущности данного типа.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Асинхронно находит сущности по заданному условию (предикату).
        /// </summary>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Асинхронно добавляет новую сущность.
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Удаляет существующую сущность.
        /// </summary>
        void Delete(T entity);
    }
}

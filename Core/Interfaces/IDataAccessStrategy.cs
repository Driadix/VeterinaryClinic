namespace Core.Interfaces
{
    public interface IDataAccessStrategy
    {
        /// <summary>
        /// Предоставляет репозиторий для работы с пользователями.
        /// </summary>
        IRepository<Core.Models.User> GetUserRepository();

        /// <summary>
        /// Предоставляет репозиторий для работы с клиентами.
        /// </summary>
        IRepository<Core.Models.Client> GetClientRepository();

        /// <summary>
        /// Предоставляет репозиторий для работы с питомцами.
        /// </summary>
        IRepository<Core.Models.Pet> GetPetRepository();

        /// <summary>
        /// Предоставляет репозиторий для работы с записями на прием.
        /// </summary>
        IRepository<Core.Models.Appointment> GetAppointmentRepository();

        /// <summary>
        /// Асинхронно сохраняет все отслеженные изменения в базе данных.
        /// </summary>
        /// <returns>Количество измененных записей.</returns>
        Task<int> SaveChangesAsync();
    }
}

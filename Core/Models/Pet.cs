namespace Core.Models
{
    public class Pet
    {
        /// <summary>
        /// Уникальный идентификатор питомца.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Кличка питомца.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Вид животного (например, "Кошка", "Собака").
        /// </summary>
        public string Species { get; set; } = string.Empty;

        /// <summary>
        /// Порода.
        /// </summary>
        public string? Breed { get; set; }

        /// <summary>
        /// Внешний ключ для связи с владельцем (Client).
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Навигационное свойство к владельцу.
        /// </summary>
        public Client Client { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство для связи с записями на прием.
        /// </summary>
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

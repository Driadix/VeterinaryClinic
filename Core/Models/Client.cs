namespace Core.Models
{
    public class Client
    {
        /// <summary>
        /// Уникальный идентификатор клиента.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Полное имя клиента.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Контактный номер телефона.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Коллекция питомцев, принадлежащих этому клиенту.
        /// </summary>
        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}

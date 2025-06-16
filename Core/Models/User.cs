namespace Core.Models
{
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин для входа в систему.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Хеш пароля для безопасного хранения.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Навигационное свойство для связи с записями на прием.
        /// </summary>
        public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Appointment
    {
        /// <summary>
        /// Уникальный идентификатор записи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата и время приема.
        /// </summary>
        public DateTime AppointmentDateTime { get; set; }

        /// <summary>
        /// Причина обращения.
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// Внешний ключ для связи с питомцем.
        /// </summary>
        public int PetId { get; set; }

        /// <summary>
        /// Навигационное свойство к питомцу.
        /// </summary>
        public Pet Pet { get; set; } = null!;

        /// <summary>
        /// Внешний ключ для связи с врачом/сотрудником.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Навигационное свойство к пользователю (врачу).
        /// </summary>
        public User User { get; set; } = null!;
    }
}

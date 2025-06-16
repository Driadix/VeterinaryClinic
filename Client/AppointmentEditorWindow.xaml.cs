using System.Windows;
using AppointmentModel = Core.Models.Appointment;
using PetModel = Core.Models.Pet;
using UserModel = Core.Models.User;

namespace Client
{
    public partial class AppointmentEditorWindow : Window
    {
        public AppointmentModel Appointment { get; private set; }

        public AppointmentEditorWindow(
            IEnumerable<PetModel> availablePets,
            IEnumerable<UserModel> availableUsers,
            AppointmentModel? appointment = null)
        {
            InitializeComponent();

            PetComboBox.ItemsSource = availablePets;
            UserComboBox.ItemsSource = availableUsers;

            if (appointment != null)
            {
                Title = "Редактирование записи";
                Appointment = appointment;

                PetComboBox.SelectedItem = availablePets.FirstOrDefault(p => p.Id == appointment.PetId);
                UserComboBox.SelectedItem = availableUsers.FirstOrDefault(u => u.Id == appointment.UserId);
                AppointmentDatePicker.SelectedDate = appointment.AppointmentDateTime;
                ReasonTextBox.Text = appointment.Reason;
            }
            else
            {
                Title = "Новая запись на прием";
                Appointment = new AppointmentModel();
                AppointmentDatePicker.SelectedDate = DateTime.Now;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (PetComboBox.SelectedItem is not PetModel selectedPet ||
                UserComboBox.SelectedItem is not UserModel selectedUser ||
                !AppointmentDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка");
                return;
            }

            Appointment.PetId = selectedPet.Id;
            Appointment.UserId = selectedUser.Id;
            Appointment.AppointmentDateTime = AppointmentDatePicker.SelectedDate.Value;
            Appointment.Reason = ReasonTextBox.Text;

            DialogResult = true;
        }
    }
}
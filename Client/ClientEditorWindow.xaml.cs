using System.Windows;
using ClientModel = Core.Models.Client;

namespace Client
{
    public partial class ClientEditorWindow : Window
    {
        public ClientModel Client { get; private set; }

        public ClientEditorWindow(ClientModel? client = null)
        {
            InitializeComponent();

            Client = client ?? new ClientModel();

            if (client != null)
            {
                Title = "Редактирование клиента";
                FullNameTextBox.Text = client.FullName;
                PhoneTextBox.Text = client.PhoneNumber;
                EmailTextBox.Text = client.Email;
            }
            else
            {
                Title = "Добавление нового клиента";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Client.FullName = FullNameTextBox.Text;
            Client.PhoneNumber = PhoneTextBox.Text;
            Client.Email = EmailTextBox.Text;

            DialogResult = true;
        }
    }
}
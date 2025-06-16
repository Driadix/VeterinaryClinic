using System.Windows;
using Client.Services;
using ClientModel = Core.Models.Client;

namespace Client
{
    public partial class MainWindow : Window
    {
        private readonly ApiClientService _apiService;

        public MainWindow()
        {
            InitializeComponent();
            _apiService = new ApiClientService();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshClientsList();
        }

        private async void LoadClientsButton_Click(object sender, RoutedEventArgs e)
        {
            await RefreshClientsList();
        }

        private async Task RefreshClientsList()
        {
            var clients = await _apiService.GetClientsAsync();
            if (clients != null)
            {
                ClientsListView.ItemsSource = clients;
            }
            else
            {
                MessageBox.Show("Не удалось загрузить данные с сервера.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            var editorWindow = new ClientEditorWindow();
            if (editorWindow.ShowDialog() == true)
            {
                await _apiService.AddClientAsync(editorWindow.Client);
                await RefreshClientsList();
            }
        }

        private async void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListView.SelectedItem is not ClientModel selectedClient)
            {
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editorWindow = new ClientEditorWindow(selectedClient);
            if (editorWindow.ShowDialog() == true)
            {
                await _apiService.UpdateClientAsync(editorWindow.Client);
                await RefreshClientsList();
            }
        }

        private async void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListView.SelectedItem is not ClientModel selectedClient)
            {
                MessageBox.Show("Пожалуйста, выберите клиента для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Вы уверены, что хотите удалить клиента '{selectedClient.FullName}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                await _apiService.DeleteClientAsync(selectedClient.Id);
                await RefreshClientsList();
            }
        }
    }
}
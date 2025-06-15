using System.Windows;
using Client.Services;

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

        private async void LoadClientsButton_Click(object sender, RoutedEventArgs e)
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
    }
}
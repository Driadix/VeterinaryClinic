using System.Windows;
using System.Windows.Controls;
using Client.Services;
using ClientModel = Core.Models.Client;
using PetModel = Core.Models.Pet;

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

        private async void ClientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsListView.SelectedItem is ClientModel selectedClient)
            {
                var pets = await _apiService.GetPetsForClientAsync(selectedClient.Id);
                PetsListView.ItemsSource = pets;
            }
            else
            {
                PetsListView.ItemsSource = null;
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

        private async void AddPetButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что выбран клиент, для которого добавляем питомца.
            if (ClientsListView.SelectedItem is not ClientModel selectedClient)
            {
                MessageBox.Show("Пожалуйста, сначала выберите клиента-владельца.", "Информация");
                return;
            }

            var editorWindow = new PetEditorWindow(selectedClient.Id);
            if (editorWindow.ShowDialog() == true)
            {
                await _apiService.AddPetAsync(editorWindow.Pet);
                await RefreshPetsForClient(selectedClient.Id);
            }
        }

        private async void EditPetButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListView.SelectedItem is not ClientModel selectedClient ||
                PetsListView.SelectedItem is not PetModel selectedPet)
            {
                MessageBox.Show("Пожалуйста, выберите питомца для редактирования.", "Информация");
                return;
            }

            var editorWindow = new PetEditorWindow(selectedClient.Id, selectedPet);
            if (editorWindow.ShowDialog() == true)
            {
                await _apiService.UpdatePetAsync(editorWindow.Pet);
                await RefreshPetsForClient(selectedClient.Id);
            }
        }

        private async void DeletePetButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListView.SelectedItem is not ClientModel selectedClient ||
                PetsListView.SelectedItem is not PetModel selectedPet)
            {
                MessageBox.Show("Пожалуйста, выберите питомца для удаления.", "Информация");
                return;
            }

            if (MessageBox.Show($"Удалить питомца '{selectedPet.Name}'?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _apiService.DeletePetAsync(selectedPet.Id);
                await RefreshPetsForClient(selectedClient.Id);
            }
        }

        private async Task RefreshPetsForClient(int clientId)
        {
            var pets = await _apiService.GetPetsForClientAsync(clientId);
            PetsListView.ItemsSource = pets;
        }
    }
}
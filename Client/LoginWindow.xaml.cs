using System.Windows;
using Client.Services;

namespace Client
{
    public partial class LoginWindow : Window
    {
        private readonly ApiClientService _apiService;

        public LoginWindow()
        {
            InitializeComponent();
            _apiService = new ApiClientService();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = await _apiService.LoginAsync(UsernameTextBox.Text, PasswordBox.Password);
            if (user != null)
            {
                var mainWindow = new MainWindow();

                Application.Current.MainWindow = mainWindow;

                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var user = await _apiService.RegisterAsync(UsernameTextBox.Text, PasswordBox.Password);
            if (user != null)
            {
                MessageBox.Show($"Пользователь {user.Username} успешно зарегистрирован. Теперь вы можете войти.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Не удалось зарегистрировать пользователя. Возможно, такой логин уже существует.", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
using System.Windows;
using Client.Services;

namespace Client
{
    public partial class LoginWindow : Window
    {
        private readonly ApiClientService _apiService;
        private bool _isProcessing = false;

        public LoginWindow()
        {
            InitializeComponent();
            _apiService = new ApiClientService();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isProcessing)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _isProcessing = true;
                LoginButton.IsEnabled = false;

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
            finally
            {
                _isProcessing = false;
                LoginButton.IsEnabled = true;
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isProcessing) return;

            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль для регистрации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _isProcessing = true;
                RegisterButton.IsEnabled = false;

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
            finally
            {
                _isProcessing = false;
                RegisterButton.IsEnabled = true;
            }
        }
    }
}
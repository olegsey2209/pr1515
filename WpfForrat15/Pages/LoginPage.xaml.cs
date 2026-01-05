using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfForrat15.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public static bool IsManagerMode { get; private set; } = false;
        private string _pin;
        public string Pin
        {
            get => _pin;
            set => _pin = value;
        }
        public LoginPage()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void LoginAsManager(object sender, RoutedEventArgs e)
        {
            if (Pin == "1234")
            {
                IsManagerMode = true;
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Неверный пин-код!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginAsVisitor(object sender, RoutedEventArgs e)
        {
            IsManagerMode = false;
            NavigationService.Navigate(new MainPage());
        }
    }
}

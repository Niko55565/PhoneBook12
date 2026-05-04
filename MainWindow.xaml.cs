using System.Windows;
using PhoneBook12.ViewModels;

namespace PhoneBook12
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; // Привязываем данные
        }
    }
}
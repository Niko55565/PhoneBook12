using System.Windows;
using PhoneBook.ViewModels;

namespace PhoneBook
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Связываем View и ViewModel через DataContext
            DataContext = new MainViewModel();
        }
    }
}
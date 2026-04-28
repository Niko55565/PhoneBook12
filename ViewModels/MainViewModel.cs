using System.Collections.ObjectModel;
using System.Windows.Input;
using PhoneBook.Models;
using PhoneBook.Commands;

namespace PhoneBook.ViewModels
{
    /// <summary>
    /// ViewModel (Модель представления) — выступает посредником между Model и View.
    /// Преобразует данные модели в формат, удобный для отображения, и предоставляет команды
    /// для обработки пользовательских действий.
    /// </summary>
    public class MainViewModel : ObservableObject
    {
        // ObservableCollection автоматически уведомляет View об изменении состава коллекции (добавлении/удалении)
        public ObservableCollection<Contact> Contacts { get; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        private string _errorMessage = "";
        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();

            // Команда без параметра
            AddCommand = new RelayCommand(AddContact, CanAddContact);

            // Команда с параметром
            DeleteCommand = new RelayCommand<object>(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            try
            {
                var contact = new Contact(Name, Phone);
                Contacts.Add(contact);

                // Очистка полей после успешного добавления
                Name = "";
                Phone = "";
                ErrorMessage = "";
            }
            catch
            {
                ErrorMessage = "Ошибка: введите корректное имя и телефон (+7XXXXXXXXXX)";
            }
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }

        // Метод удаления принимает выбранный контакт в качестве параметра
        private void DeleteContact(object? parameter)
        {
            if (parameter is Contact contact)
            {
                Contacts.Remove(contact);
            }
        }

        private bool CanDeleteContact(object? parameter)
        {
            return parameter is Contact; // Кнопка активна, если параметр - это контакт (не null)
        }
    }
}
using PhoneBook.Commands;
using PhoneBook.Models;
using PhoneBook.ViewModels;
using PhoneBook12.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PhoneBook12.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        public ObservableCollection<Contact> Contacts { get; }

        private string _name = "";
        public string Name { get => _name; set => Set(ref _name, value); }

        private string _phone = "";
        public string Phone { get => _phone; set => Set(ref _phone, value); }

        private Contact? _selectedContact;
        public Contact? SelectedContact { get => _selectedContact; set => Set(ref _selectedContact, value); }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(IDialogService dialogService) // Внедрение через конструктор
        {
            _dialogService = dialogService;
            Contacts = new ObservableCollection<Contact>();
            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<object>(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            if (Contacts.Any(c => c.Phone == Phone))
            {
                _dialogService.ShowWarning("Такой номер уже есть в книге!");
                return;
            }

            try
            {
                var contact = new Contact(Name, Phone);
                Contacts.Add(contact);
                _dialogService.ShowInfo("Контакт успешно добавлен!");
                Name = ""; Phone = "";
            }
            catch { _dialogService.ShowError("Ошибка валидации данных!"); }
        }

        private bool CanAddContact() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);

        private void DeleteContact(object? parameter)
        {
            if (parameter is Contact contact && _dialogService.ShowConfirmation($"Удалить {contact.Name}?"))
            {
                Contacts.Remove(contact);
            }
        }

        private bool CanDeleteContact(object? parameter) => parameter is Contact;
    }
}
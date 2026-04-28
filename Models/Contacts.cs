using PhoneBook.ViewModels;
using System;
using System.Text.RegularExpressions;

namespace PhoneBook.Models
{
    /// <summary>
    /// Model (Модель) — содержит бизнес-данные приложения (сущность контакта) и правила их валидации.
    /// Модель полностью независима от View и не знает, как отображаются данные.
    /// </summary>
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        public Contact(string name, string phone)
        {
            _name = name;
            _phone = phone;

            if (!Validate())
                throw new ArgumentException("Некорректные данные");
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        // Бизнес-логика валидации данных
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            // Валидация: начинается с +7 и имеет 10 цифр после, либо просто 10 цифр
            var regex = new Regex(@"^(\+7\d{10}|\d{10})$");
            return regex.IsMatch(Phone);
        }
    }
}
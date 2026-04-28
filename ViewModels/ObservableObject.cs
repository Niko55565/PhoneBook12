using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneBook.ViewModels
{
    /// <summary>
    /// Базовый класс MVVM, реализующий INotifyPropertyChanged.
    /// Необходим для уведомления View об изменении свойств в ViewModel и Model.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(name);
            return true;
        }
    }
}
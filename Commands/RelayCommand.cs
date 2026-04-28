using System;
using System.Windows.Input;

namespace PhoneBook.Commands
{
    /// <summary>
    /// Реализация интерфейса ICommand.
    /// Позволяет ViewModel обрабатывать действия пользователя (нажатия кнопок) из View,
    /// не создавая жесткой зависимости от визуальных элементов интерфейса.
    /// </summary>

    // Команда без параметра (используется для AddCommand)
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter = null)
            => _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter = null)
        {
            if (CanExecute(parameter))
                _execute();
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    // Команда с параметром (используется для DeleteCommand)
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
            => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter)
        {
            if (CanExecute(parameter))
                _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
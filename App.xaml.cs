using Microsoft.Extensions.DependencyInjection;
using PhoneBook;
using PhoneBook12.Services;
using PhoneBook12.ViewModels;
using System;
using System.Windows;

namespace PhoneBook12
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            // Регистрируем сервисы (Singleton - один на все время)
            services.AddSingleton<IDialogService, DialogService>();

            // Регистрируем ViewModel и Окно (Transient - каждый раз новый)
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
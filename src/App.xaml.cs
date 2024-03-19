using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetService<MainWindow>();
            if (mainWindow != null)
            {
                mainWindow.Show();
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddOptions();
            services.Configure<AppSettings>(settings =>
            {
                configuration.GetSection("GlobalSettings").Bind(settings);
            });

            services.AddTransient<MainWindow>();
            services.AddTransient<AddBookWindow>();
            services.AddTransient<EditBookWindow>();

            services.AddTransient<Factories.IBookFactory, Factories.Impl.BookFactory>();
            
            services.AddTransient<Repositories.IBooksRepository, Repositories.Impl.BooksRepository>();

            services.AddTransient<Services.IBooksService, Services.Impl.BooksService>();
        }

    }
}

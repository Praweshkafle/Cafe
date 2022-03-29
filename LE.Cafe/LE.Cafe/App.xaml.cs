using LE.Cafe.Services;
using LE.Cafe.Services.Implementation;
using LE.Cafe.Services.Interfaces;
using LE.Cafe.ViewModels;
using LE.Cafe.Views;
using LE.Cafe.Views.Home;
using LE.Cafe.Views.Login;
using LE.Cafe.Views.Order;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LE.Cafe
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public App(Action<IServiceCollection> addPlatformServices = null)
        {
            InitializeComponent();
            SetupServices(addPlatformServices);
            MainPage = new LoginPage();
        }

        private void SetupServices(Action<IServiceCollection> addPlatformServices = null)
        {
            var services = new ServiceCollection();
            addPlatformServices?.Invoke(services);
            services.AddTransient<LoginViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<OrderViewModel>();
            services.AddSingleton<ApiServices, ApiServiceImpl>();
            services.AddTransient<ProductServices, ProductServicesImpl>();
            //add services here
            ServiceProvider =services.BuildServiceProvider();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

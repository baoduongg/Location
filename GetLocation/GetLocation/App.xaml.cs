using System;
using GetLocation.View;
using GetLocation.ViewModel;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetLocation
{

    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>(nameof(NavigationPage));

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>(nameof(LoginPageViewModel));

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(nameof(MainPageViewModel));
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>(nameof(HomePageViewModel));
            containerRegistry.RegisterForNavigation<LocationPage, LocationPageViewModel>(nameof(LocationPageViewModel));
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(LoginPageViewModel)}");
        }
    }
}

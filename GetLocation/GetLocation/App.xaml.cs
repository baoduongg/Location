using System;
using GetLocation.Extensions;
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

            containerRegistry.RegisterForNavigation<NavigationBase<MainPage>>($"Nav{nameof(MainPage)}");
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(nameof(MainPageViewModel));

            containerRegistry.RegisterForNavigation<HomePage>(nameof(HomePage));

            containerRegistry.RegisterForNavigation<NavigationBase<LocationPage>>($"Nav{nameof(LocationPage)}");
            containerRegistry.RegisterForNavigation<LocationPage, LocationPageViewModel>(nameof(LocationPageViewModel));
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationShortcuts.InitFirstPage(NavigationService);
        }
    }
    public class NavigationBase<TView> : NavigationPage
    {
        public NavigationBase(TView root) : base(root as Page)
        {
            var page = root as Page;

            Title = page.Title;
            IconImageSource = page.IconImageSource;
        }
    }
}

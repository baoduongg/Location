using System;
using GetLocation.Extensions;
using GetLocation.View;
using GetLocation.ViewModel;
using Prism.Ioc;
using Xamarin.Forms;

namespace GetLocation
{
    public class NavigationRegistry : IIocRegistry
    {
        public void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>(nameof(NavigationPage));

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>(nameof(LoginPageViewModel));

            containerRegistry.RegisterForNavigation<NavigationBase<MainPage>>($"Nav{nameof(MainPage)}");

            containerRegistry.RegisterForNavigation<NavigationBase<MenuPage>>($"Nav{nameof(MenuPage)}");
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>(nameof(MenuPageViewModel));

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(nameof(MainPageViewModel));

            containerRegistry.RegisterForNavigation<HomePage>(nameof(HomePage));

            containerRegistry.RegisterForNavigation<NavigationBase<LocationPage>>($"Nav{nameof(LocationPage)}");
            containerRegistry.RegisterForNavigation<LocationPage, LocationPageViewModel>(nameof(LocationPageViewModel));
        }
    }
}

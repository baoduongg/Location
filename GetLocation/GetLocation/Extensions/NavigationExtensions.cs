using System;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using Xamarin.Forms;

namespace GetLocation.Extensions
{
    public static class NavigationExtentions
    {
        public static INavigationService CreateNavigationService(this Page target)
        {
            var app = Application.Current as PrismApplication;
            var navigationService = (app.Container as IContainerExtension).CreateNavigationService(target);
            return navigationService;
        }
    }
}

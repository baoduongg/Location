using System;
using System.Linq;
using System.Threading.Tasks;
using GetLocation.View;
using GetLocation.ViewModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace GetLocation.Extensions
{
    public static class NavigationShortcuts 
    {
        private const string RemovePagePrefix = "../"; // defined by prism
        public static Task InitFirstPage(INavigationService navigationService)
        {
            if (navigationService == null)
                throw new ArgumentNullException(nameof(navigationService));
            var url = $"/{nameof(NavigationPage)}/{nameof(LoginPageViewModel)}";
            return navigationService.NavigateAsync(url);
        }
        public static async Task<INavigationResult> GoToLoginPageAsync()
        {
            var currentPage = GetCurrentPageOfMainStack();
            ViewModelBase.AcessToken = null;
            var prefixUrl = string.Join(string.Empty, currentPage.Navigation.NavigationStack.Select(p => RemovePagePrefix).ToArray());// remove all pre-page
            prefixUrl += nameof(LoginPageViewModel);
            var navigationService = NavigationExtentions.CreateNavigationService(currentPage);
            var result = await navigationService.NavigateAsync(prefixUrl, animated: false);
            return result;
        }
        public static async Task<INavigationResult> GoToMainPageAsync()
        {
            var currentPage = GetCurrentPageOfMainStack();
            var prefixUrl = string.Join(string.Empty, currentPage.Navigation.NavigationStack.Select(p => RemovePagePrefix).ToArray());// remove all pre-page
            var navParams = new NavigationParameters
            {
                { KnownNavigationParameters.CreateTab, $"Nav{nameof(LocationPage)}" }, // use NavigationPage as a tab item
                { KnownNavigationParameters.CreateTab, $"Nav{nameof(MainPage)}"},
                { KnownNavigationParameters.CreateTab, $"Nav{nameof(MenuPage)}"},
                { KnownNavigationParameters.SelectedTab, $"Nav{nameof(LocationPage)}" },
            };
            var queryUri = $"{prefixUrl}{nameof(HomePage)}{navParams}";
            var navigationService = NavigationExtentions.CreateNavigationService(currentPage);
            var result = await navigationService.NavigateAsync(queryUri, animated: false);
            return result;
        }
        private static Page GetCurrentPageOfMainStack()
        {
            var rootNavigationPage = Application.Current.MainPage as NavigationPage;
            if (rootNavigationPage == null)
                throw new ArgumentException("The main navigation flow was changed! please review your logic.", nameof(rootNavigationPage));
            var lastPage = rootNavigationPage.Navigation.NavigationStack.LastOrDefault();
            return lastPage;
        }
    }
}


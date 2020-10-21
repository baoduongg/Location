using System;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using GetLocation.Extensions;

namespace GetLocation.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {

        private IPageDialogService _pageDialogService;
        public LoginPageViewModel(INavigationService navigationService,IPageDialogService pageDialogService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        #region LoginCommand
        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand => _loginCommand = new DelegateCommand(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
           
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var cross = CrossGeolocator.Current.IsGeolocationAvailable;
                await NavigationShortcuts.GoToMainPageAsync();
            }
            else
                await _pageDialogService.DisplayAlertAsync("", "Turn on Internet", "OK");
            IsBusy = false;
        });
        #endregion
    }
}

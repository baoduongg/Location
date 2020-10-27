using System;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using GetLocation.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using GetLocation.Model;
using GetLocation.Interfaces.Login;

namespace GetLocation.ViewModel
{

    public class LoginPageViewModel : ViewModelBase
    {
        private IPageDialogService _pageDialogService;
        private ILoginService _loginService;
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,ILoginService loginService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _loginService = loginService;
            UserName = "duong_dev";
            Password = "duong@123";
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
                var reponse = await _loginService.Login(UserName,Password);
                if (reponse.IsSuccess)
                {
                    //await _loginService.SaveIdAsync(UserName);
                    //await _loginService.SavePasswordAsync(Password);
                    AcessToken = reponse.AccessToken;
                    await NavigationShortcuts.GoToMainPageAsync();
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("", "Login Fail", "OK");
                    //await _loginService.SaveIdAsync(null);
                    //await _loginService.SavePasswordAsync(null);
                }
            }
            else
                await _pageDialogService.DisplayAlertAsync("", "Turn on Internet", "OK");
            IsBusy = false;
        });
        #endregion
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            //var user = await _loginService.GetIdAsync();
            //if (user != null)
            //{
            //    UserName = user;
            //}
            //else
            //{
            //    UserName = null;
            //}
            //var pass = await _loginService.GetPasswordAsync();
            //if (pass != null)
            //{
            //    Password = pass;
            //}
            //else
            //{
            //    Password = null;
            //}
        }
    }
}

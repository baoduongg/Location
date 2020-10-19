using System;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace GetLocation.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
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
            await _navigationService.NavigateAsync(nameof(HomePageViewModel));
            IsBusy = false;
        });
        #endregion
    }
}

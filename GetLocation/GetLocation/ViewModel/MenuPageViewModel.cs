using System;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using GetLocation.Extensions;

namespace GetLocation.ViewModel
{
    public class MenuPageViewModel : ViewModelBase
    {
       
        public MenuPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        #region LogoutCommand
        private DelegateCommand _logoutCommand;
        public DelegateCommand LogoutCommand => _logoutCommand = new DelegateCommand(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            await NavigationShortcuts.GoToLoginPageAsync();
            IsBusy = false;
        });
        #endregion
    }
}


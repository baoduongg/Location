using System;
using GetLocation.Extensions;
using Prism.Commands;
using Prism.Navigation;

namespace GetLocation.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
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

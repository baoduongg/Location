using System;
using System.Collections.ObjectModel;
using GetLocation.Extensions;
using Prism.Commands;
using Prism.Navigation;
using Refit;
using System.Threading.Tasks;
using System.Threading;
using GetLocation.Model;
using GetLocation.Interfaces.Location;
using Xamarin.Forms.Maps;
using GetLocation.Interfaces.List;
using GetLocation.Interfaces;

namespace GetLocation.ViewModel
{
    public class MainPageViewModel : ViewModelBase, ITabSelectedAware
    {
        private IListService _listService;
        public MainPageViewModel(INavigationService navigationService,IListService listService) : base(navigationService)
        {
            _listService = listService;
        }
        CancellationTokenSource _tokenLocation;

        private ObservableCollection<Message> _locations;
        public ObservableCollection<Message> Locations
        {
            get { return _locations; }
            set { SetProperty(ref _locations, value); }
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
        async Task GetLocation()
        {
            Locations = new ObservableCollection<Message>();
            var reponse =  await _listService.GetList(AcessToken);
            if (reponse.Success > 0)
            {
                foreach (var item in reponse.Message)
                {
                    Locations.Add(item);
                }
            }

        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public async void TabSelected(INavigationParameters navigationParameters)
        {
            await GetLocation();
        }

        public void TabUnSelected(INavigationParameters navigationParameters)
        {
        }
    }
}

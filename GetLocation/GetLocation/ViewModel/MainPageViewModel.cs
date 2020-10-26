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

namespace GetLocation.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        CancellationTokenSource _tokenLocation;

        private ObservableCollection<LocationModel> _locations;
        public ObservableCollection<LocationModel> Locations
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
            _tokenLocation?.Cancel();
            _tokenLocation = new CancellationTokenSource();
            Locations = new ObservableCollection<LocationModel>();

            var apiResponse = RestService.For<ILocationApi>("https://landber.com");
            var request = new LocationRequest();
            request.CodeTinh = "SG";
            var reponse = await apiResponse.GetListLocation(request, _tokenLocation.Token);

            if (reponse.Data[0] != null)
            {
                foreach (var item in reponse.Data[0])
                {
                    Locations.Add(new LocationModel($"{item.Latitude}", $"{item.Longitude}", new Position(item.Latitude, item.Longitude)));
                }
            }
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await GetLocation();
        }
    }
}

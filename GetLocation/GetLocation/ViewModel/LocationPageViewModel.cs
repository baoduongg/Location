using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GetLocation.Model;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace GetLocation.ViewModel
{
    public class LocationPageViewModel : ViewModelBase
    {
        public LocationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private ObservableCollection<LocationModel> _locations;
        public ObservableCollection<LocationModel> Locations
        {
            get { return _locations; }
            set { SetProperty(ref _locations, value); }
        }
        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)

                    });
                }
                if (location == null)
                    Position = new Position();
                else
                    Position = new Position(location.Latitude, location.Longitude);
                Locations.Add(new LocationModel { Position = Position });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"bug {ex.Message}");
            }

        }
    }
}

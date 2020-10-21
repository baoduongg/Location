using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using GetLocation.Model;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace GetLocation.ViewModel
{
    public class LocationPageViewModel : ViewModelBase
    {
        private ObservableCollection<LocationModel> _locations;
        public ObservableCollection<LocationModel> Locations
        {
            get { return _locations; }
            set { SetProperty(ref _locations, value); }
        }

        public LocationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Locations = new ObservableCollection<LocationModel>();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            AddLocation();
        }
        #region AddLocationCommand
        private DelegateCommand _addLocationCommand;
        public DelegateCommand AddLocationCommand => _addLocationCommand = new DelegateCommand(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            await GetLocation();
            AddLocation();
            IsBusy = false;
        });
        #endregion
        private void AddLocation()
        {
            Locations.Add(NewLocation());
        }

        private LocationModel NewLocation()
        {
            return new LocationModel(
               Adress,
               "",
               Position);
        }
        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }
        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set { SetProperty(ref _adress, value); }
        }
        async Task GetLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.High,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }
            if (location != null)
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    Adress = $"{placemark.FeatureName} {placemark.SubLocality} {placemark.SubAdminArea} {placemark.AdminArea} {placemark.CountryName}";
                    Position = new Position(placemark.Location.Latitude, placemark.Location.Longitude);
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
    static class RandomPosition
    {
        static Random Random = new Random(Environment.TickCount);

        public static Position Next(Position position, double latitudeRange, double longitudeRange)
        {
            return new Position(
                position.Latitude + (Random.NextDouble() * 2 - 1) * latitudeRange,
                position.Longitude + (Random.NextDouble() * 2 - 1) * longitudeRange);
        }
    }
}

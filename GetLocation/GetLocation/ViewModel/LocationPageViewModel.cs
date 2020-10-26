using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using GetLocation.Model;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Refit;
using GetLocation.Interfaces.Location;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

namespace GetLocation.ViewModel
{
    public class LocationPageViewModel : ViewModelBase
    {
        CancellationTokenSource _tokenLocation;
        private ObservableCollection<LocationModel> _locations;
        public ObservableCollection<LocationModel> Locations
        {
            get { return _locations; }
            set { SetProperty(ref _locations, value); }
        }

        private MapSpan _mapSpan;
        public MapSpan GetMapSpan
        {
            get { return _mapSpan; }
            set { SetProperty(ref _mapSpan, value); }
        }


        public LocationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }

        #region AddLocationCommand
        private DelegateCommand _addLocationCommand;
        public DelegateCommand AddLocationCommand => _addLocationCommand = new DelegateCommand(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            //await GetLocation();
            await AddLocation();
            IsBusy = false;
        });
        #endregion
        private async Task AddLocation()
        {
            _tokenLocation?.Cancel();
            _tokenLocation = new CancellationTokenSource();
            Locations = new ObservableCollection<LocationModel>();
            GetPolyline = new Polyline
            {
                StrokeWidth = 8,
                StrokeColor = Color.FromHex("#1BA1E2"),
            };
            GetPolygon = new Polygon
            {
                StrokeWidth = 8,
                StrokeColor = Color.FromHex("#1BA1E2"),
                FillColor = Color.FromHex("#881BA1E2")
            };

            var apiResponse = RestService.For<ILocationApi>("https://landber.com");
            var request = new LocationRequest();
            request.CodeTinh = "SG";
            var reponse = await apiResponse.GetListLocation(request,_tokenLocation.Token);
           
            if (reponse.Data[0] != null)
            {
                foreach (var item in reponse.Data[0])
                {
                    //Locations.Add(new LocationModel("", "", new Position(item.Latitude, item.Longitude)));
                    //GetPolyline.Geopath.Add(new Position(item.Latitude, item.Longitude));
                    GetPolygon.Geopath.Add(new Position(item.Latitude, item.Longitude));
                }
            }
        }
        private Polygon _getPolygon;
        public Polygon GetPolygon
        {
            get { return _getPolygon; }
            set { SetProperty(ref _getPolygon, value); }
        }
        private Polyline _getPolyLine;
        public Polyline GetPolyline
        {
            get { return _getPolyLine; }
            set { SetProperty(ref _getPolyLine, value); }
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
            //var location = await Geolocation.GetLastKnownLocationAsync();
            //if (location == null)
            //{
            //    location = await Geolocation.GetLocationAsync(new GeolocationRequest
            //    {
            //        DesiredAccuracy = GeolocationAccuracy.High,
            //        Timeout = TimeSpan.FromSeconds(30)
            //    });
            //}
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.High,
                Timeout = TimeSpan.FromSeconds(30)
            });
            if (location != null)
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    Adress = $"{placemark.FeatureName} {placemark.SubLocality} {placemark.SubAdminArea} {placemark.AdminArea} {placemark.CountryName}";
                    Position = new Position(placemark.Location.Latitude, placemark.Location.Longitude);
                    GetMapSpan = new MapSpan(new Position(placemark.Location.Latitude, placemark.Location.Longitude), 0.1, 0.1);
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

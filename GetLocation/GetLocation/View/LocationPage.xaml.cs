using System;
using System.Collections.Generic;
using System.Threading;
using GetLocation.ViewModel;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GetLocation.View
{
    public partial class LocationPage : PageBase
    {
        public LocationPage()
        {
            InitializeComponent();

            CancellationTokenSource cts;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    cts = new CancellationTokenSource();
                    var location = await Geolocation.GetLocationAsync(request, cts.Token);


                    if (location != null)
                    {
                        MapUI.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude),
                                                          Distance.FromMiles(1)));
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    //await DisplayAlert("Error1", fnsEx.ToString(), "ok");
                }
                catch (PermissionException pEx)
                {
                    //await DisplayAlert("Error2", pEx.ToString(), "ok");
                }
                catch (Exception ex)
                {
                    //await DisplayAlert("Error3", ex.ToString(), "ok");
                }
            });
        }

    }
}

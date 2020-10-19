using System;
using Prism.Mvvm;
using Xamarin.Forms.Maps;

namespace GetLocation.Model
{
    public class LocationModel : BindableBase
    {
        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        private string _placeName;
        public string PlaceName
        {
            get { return _placeName; }
            set { SetProperty(ref _placeName, value); }
        }
        public LocationModel(string address, string description, Position position)
        {
            Address = address;
            Description = description;
            Position = position;
        }
    }
}

using System;
using Xamarin.Forms.Maps;
using Xamarin.Forms;

namespace GetLocation.View
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty CustomPolylineProperty
            = BindableProperty.Create("CustomPolyline", typeof(Polyline), typeof(CustomMap),
                                        propertyChanged: (bindableObject, oldValue, newValue) =>
                                        {
                                            Map map = bindableObject as Map;
                                            map.MapElements.Clear();
                                            map.MapElements.Add((Polyline)newValue);
                                        });

        public Polyline CustomPolyline
        {
            get => (Polyline)GetValue(CustomPolylineProperty);
            set => SetValue(CustomPolylineProperty, value);
        }

        public static readonly BindableProperty CustomPolygonProperty
           = BindableProperty.Create("CustomPolygon", typeof(Polygon), typeof(CustomMap),
                                       propertyChanged: (bindableObject, oldValue, newValue) =>
                                       {
                                           Map map = bindableObject as Map;
                                           map.MapElements.Clear();
                                           map.MapElements.Add((Polygon)newValue);
                                       });

        public Polyline CustomPolygon
        {
            get => (Polyline)GetValue(CustomPolygonProperty);
            set => SetValue(CustomPolygonProperty, value);
        }



        public MapSpan MapSpan
        {
            get { return (MapSpan)GetValue(MapSpanProperty); }
            set { SetValue(MapSpanProperty, value); }
        }

        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
                                                         propertyName: "MapSpan",
                                                         returnType: typeof(MapSpan),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         validateValue: null,
                                                         propertyChanged: MapSpanPropertyChanged);

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as CustomMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        public CustomMap() : base()
        {

        }
        public CustomMap(MapSpan region) : base(region)
        { 

        }
    }
}

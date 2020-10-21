using System;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace GetLocation.View
{
    public class HomePage : Xamarin.Forms.TabbedPage, IDestructible
    {

        public HomePage()
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSwipePagingEnabled(false);
            On<Android>().SetIsLegacyColorModeEnabled(false);
            On<Android>().SetIsSmoothScrollEnabled(false);
            // Number of pages that should be retained to either side of the current page in the view hierarchy in an idle state
            On<Android>().SetOffscreenPageLimit(5);

            BarBackgroundColor = Colors.BarBackgroundColor;
            SelectedTabColor = Colors.SelectedTabColor;
            UnselectedTabColor = Colors.UnselectedTabColor;

            On<iOS>().SetTranslucencyMode(TranslucencyMode.Opaque);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);

        }
        public void Destroy()
        {
        }
    }
    public class Colors
    {
        public static Color BarBackgroundColor = Color.FromRgb(17, 17, 17);
        public static Color BarBackground2Color = Color.FromRgb(0, 0, 0);
        public static Color SelectedTabColor = Color.FromRgb(219, 219, 219);
        public static Color UnselectedTabColor = Color.FromRgba(255, 255, 255, .5);
    }
}

using System;
using GetLocation.Interfaces;
using Prism.Common;
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
            _lastTabIndex = -1;
            _tabSelectedParameters = null;
        }
        INavigationParameters _tabSelectedParameters;
        public void SyncTabSelectedParameters(INavigationParameters navigationParameters)
        {
            _tabSelectedParameters = navigationParameters;
        }
        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            var currentPage = PageUtilities.GetCurrentPage(CurrentPage);
            var parameters = _tabSelectedParameters;
            if (parameters == null)
                parameters = new NavigationParameters();

            /*
             * After call UnSelected of old tab, call Selected of current tab
             */
            InvokeTabUnSelected(parameters);

            PageUtilities.InvokeViewAndViewModelAction<ITabSelectedAware>(currentPage,
                tabSelectedAware => tabSelectedAware.TabSelected(parameters));

            _tabSelectedParameters = null;
        }
        int _lastTabIndex = -1;

        void InvokeTabUnSelected(INavigationParameters pr)
        {
            var currentTabIndex = Children.IndexOf(CurrentPage);
            if (currentTabIndex != _lastTabIndex)
            {
                if (_lastTabIndex != -1)
                {
                    var lastTabPage = Children[_lastTabIndex];
                    var topUnSelectedPage = PageUtilities.GetCurrentPage(lastTabPage);
                    PageUtilities.InvokeViewAndViewModelAction<ITabSelectedAware>(topUnSelectedPage,
                        tabSelectedAware => tabSelectedAware.TabUnSelected(pr));
                }
                _lastTabIndex = currentTabIndex;
            }
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

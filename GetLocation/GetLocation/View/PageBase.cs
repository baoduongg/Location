using System;
using GetLocation.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace GetLocation.View
{
    public class PageBase : ContentPage
    {
        public PageBase()
        {
            this.SetBinding(IsBusyProperty, nameof(ViewModelBase.IsBusy), BindingMode.TwoWay);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Prism.Mvvm.ViewModelLocator.SetAutowireViewModel(this, true);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(UseSafeArea);
        }
        public virtual bool UseSafeArea => true;
    }
}

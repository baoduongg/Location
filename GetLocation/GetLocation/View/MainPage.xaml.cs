using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration;

namespace GetLocation.View
{
    public partial class MainPage : PageBase
    {
        public MainPage()
        {
            InitializeComponent();
            lv.On<iOS>().SetSeparatorStyle(SeparatorStyle.FullWidth);
            lv.On<iOS>().SetRowAnimationsEnabled(false);
        }
    }
}

using System;
using Prism.Navigation;

namespace GetLocation.Interfaces
{
    public interface ITabSelectedAware
    {
        void TabSelected(INavigationParameters navigationParameters);
        void TabUnSelected(INavigationParameters navigationParameters);
    }
}

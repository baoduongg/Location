using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace GetLocation.ViewModel
{
    public class ViewModelBase:BindableBase,
        INavigatedAware,
        IPageLifecycleAware,
        IApplicationLifecycleAware,
        IDestructible,
        IInitializeAsync
        
    {
        public static string AcessToken = string.Empty;
        public readonly INavigationService _navigationService;
        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public virtual void Destroy()
        {
        }

        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnResume()
        {
        }

        public virtual void OnSleep()
        {
        }
        public void SetBasicAuthHeader(WebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }
    }
}

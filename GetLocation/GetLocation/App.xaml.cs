using System;
using System.Threading.Tasks;
using GetLocation.Extensions;
using GetLocation.Interfaces.Migration;
using GetLocation.View;
using GetLocation.ViewModel;
using Prism;
using Prism.Ioc;
using Prism.Services;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetLocation
{

    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Ioc.Current.RegisterTypes(containerRegistry,
               new ServicesRegistry(), // register business services
               new NavigationRegistry()); // register for navigation
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            var migrationService = Container.Resolve<IMigrationService>();
            //Task.Run(async () =>
            //{
            //    await migrationService.PrepareRestoredData();
            //    await migrationService.MigrateAsync();
            //}).Wait();
            await NavigationShortcuts.InitFirstPage(NavigationService);
        }
    }
    public class NavigationBase<TView> : NavigationPage
    {
        public NavigationBase(TView root) : base(root as Page)
        {
            var page = root as Page;

            Title = page.Title;
            IconImageSource = page.IconImageSource;
        }
    }
}

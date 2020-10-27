using System;
using GetLocation.Interfaces.List;
using GetLocation.Interfaces.Login;
using GetLocation.Interfaces.Migration;
using GetLocation.Interfaces.RecordInfo;
using GetLocation.Interfaces.Storage;
using Prism.Ioc;

namespace GetLocation.Extensions
{
    public class ServicesRegistry : IIocRegistry
    {
        public void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoginService, LoginService>();
            containerRegistry.Register<IRecordInfoService, RecordInfoService>();
            containerRegistry.Register<IStorageService, StorageService>();
            containerRegistry.Register<IMigrationService, MigrationService>();
            containerRegistry.Register<IListService, ListService>();
        }
    }
}

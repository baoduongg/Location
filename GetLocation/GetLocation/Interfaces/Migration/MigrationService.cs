using System;
using System.Threading.Tasks;
using GetLocation.Interfaces.Login;
using GetLocation.Interfaces.Storage;

namespace GetLocation.Interfaces.Migration
{
    public class MigrationService : IMigrationService
    {
        #region keys
        const string StorageVersionKey = "GetLocation.Interfaces.Migration.MigrationService.StorageVersionKey";
        const string StartFlagKey = "GetLocation.Interfaces.Migration.MigrationService.StartFlagKey";
        #endregion

        private readonly IStorageService _storageService;
        private readonly ILoginService _loginService;
        public MigrationService(IStorageService storageService, ILoginService loginService)
        {
            _loginService = loginService;
            _storageService = storageService;
        }

        public async Task MigrateAsync()
        {
            // Init storage version.
            var storageVersion = await _storageService.GetOrCreateAsync(StorageVersionKey, 0);

            int lastestStorageVersion = 1; // increment lastedStorageVersion when need migration data.

            if (storageVersion == 0)
            {
                // Update and save storage version.
                await _storageService.SetAsync(StorageVersionKey, lastestStorageVersion);
                return;
            }

            if (storageVersion != 0 && storageVersion <= lastestStorageVersion)
            {
                for (int i = storageVersion; i < lastestStorageVersion; i++)
                {
                    await MigrateDataAsync(i + 1);
                }
                await _storageService.SetAsync(StorageVersionKey, lastestStorageVersion);
            }
        }

        public async Task PrepareRestoredData()
        {
            var startupflagProperty = await _storageService.GetOrCreateAsync(StartFlagKey, false);
            // System.AggregateException: One or more errors occurred. (Failed to obtain information about key) ---> Java.Security.UnrecoverableKeyException: Failed to obtain information about key ---> Java.Lang.Exception: Invalid key blob
            var startupflagSecure = await _storageService.GetSecureOrCreateAsync(StartFlagKey, false);

            if (startupflagProperty != startupflagSecure)
            {
                _loginService.DeleteId();
                _loginService.DeletePassword();
            }

            await _storageService.SetAsync(StartFlagKey, true);
            await _storageService.SetSecureAsync(StartFlagKey, true);
        }

        private Task MigrateDataAsync(int newVersion)
        {
            // migration implementation
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Threading.Tasks;

namespace GetLocation.Interfaces.Migration
{
    public interface IMigrationService
    {
        Task MigrateAsync();

        Task PrepareRestoredData();
    }
}

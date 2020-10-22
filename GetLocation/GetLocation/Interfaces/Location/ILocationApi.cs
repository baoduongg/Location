using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Refit;

namespace GetLocation.Interfaces.Location
{
    public interface ILocationApi
    {
        [Post("/api/v2/getBoundary")]
        Task<LocationResponse> GetListLocation([Body]LocationRequest request, CancellationToken token);
    }
}

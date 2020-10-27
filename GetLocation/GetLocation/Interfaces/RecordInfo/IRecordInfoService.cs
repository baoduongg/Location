using System;
using System.Threading;
using System.Threading.Tasks;

namespace GetLocation.Interfaces.RecordInfo
{
    public interface IRecordInfoService
    {
        Task<RecordInfoResponse> GetRecordInfo(RecordInfoRequest request, CancellationToken token);
    }
}

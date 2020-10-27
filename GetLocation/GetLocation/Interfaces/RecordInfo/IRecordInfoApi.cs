using System;
using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace GetLocation.Interfaces.RecordInfo
{
    public interface IRecordInfoApi
    {
        [Post("/web/webservice/get-list-record-info-by-md5")]
        Task<RecordInfoResponse> GetRecordInfor([Body] RecordInfoRequest request, CancellationToken token);
    }
}

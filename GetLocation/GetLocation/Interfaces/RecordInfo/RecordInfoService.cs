using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GetLocation.Interfaces.Location;
using Refit;

namespace GetLocation.Interfaces.RecordInfo
{
    public class RecordInfoService : IRecordInfoService
    {
        public async Task<RecordInfoResponse> GetRecordInfo(RecordInfoRequest request, CancellationToken token)
        {
            var apiResponse = RestService.For<IRecordInfoApi>("https://genesys.topcall.com.vn");
            var reponse = await apiResponse.GetRecordInfor(request, token);
            return reponse; 
        }
    }
}

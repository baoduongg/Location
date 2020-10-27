using System;
using Newtonsoft.Json;

namespace GetLocation.Interfaces.RecordInfo
{
    public class RecordInfoRequest
    {
        [JsonProperty("md5_code")]
        public string Md5Code { get; set; }
    }
}

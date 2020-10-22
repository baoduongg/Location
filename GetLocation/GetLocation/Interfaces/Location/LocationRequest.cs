using System;
using Newtonsoft.Json;

namespace GetLocation.Interfaces.Location
{
    public class LocationRequest
    {
        [JsonProperty("codeTinh")]
        public string CodeTinh { get; set; }

        [JsonProperty("codeHuyen")]
        public string CodeHuyen { get; set; }
    }
}

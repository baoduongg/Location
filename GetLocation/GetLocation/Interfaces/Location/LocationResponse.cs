using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GetLocation.Interfaces.Location
{
    public class LocationResponse
    {
        [JsonProperty("data")]
        public IList<IList<PositionApi>> Data { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
    public class PositionApi
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GetLocation.Interfaces.List
{
    public class ListReponse
    {
        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("message")]
        public IList<Message> Message { get; set; }
    }
    public class Message
    {

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("billsec")]
        public int Billsec { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("md5_code")]
        public string Md5Code { get; set; }
    }

}

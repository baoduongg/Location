using System;
using Newtonsoft.Json;

namespace GetLocation.Interfaces.Login
{
    public class LoginReponse
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public bool IsSuccess { get; set; }
    }
}

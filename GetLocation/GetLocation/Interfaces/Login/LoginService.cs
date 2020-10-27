using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GetLocation.Interfaces.Storage;
using Newtonsoft.Json;
using Prism.Services;

namespace GetLocation.Interfaces.Login
{
    public class LoginService : ILoginService
    {
        private readonly IDeviceService _deviceService;
        private readonly IStorageService _storageService;
        const string LoginUserKey = "GetLocation.Interfaces.Login.LoginService.LoginUserKey";
        const string LoginPasswordKey = "GetLocation.Interfaces.Login.LoginService.LoginPasswordKey";
        public LoginService(IDeviceService deviceService, IStorageService storageService)
        {
            _deviceService = deviceService;
            _storageService = storageService;
        }
        public string AccountId { get; private set; }

        public async Task<LoginReponse> Login(string Username, string Password)
        {
            LoginReponse userInfo = new LoginReponse();
            HttpClient _client;

            var authData = string.Format("{0}:{1}", Username, Password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            Uri url = new Uri("https://genesys.topcall.com.vn/web/auth/login");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = url,
            };
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                // convert reponse to string json
                var reponseString = await response.Content.ReadAsStringAsync();
                // convert string json to object
                userInfo = JsonConvert.DeserializeObject<LoginReponse>(reponseString);
                userInfo.IsSuccess = true;
                return userInfo;
            }
            else
            {
                userInfo.IsSuccess = false;
                return userInfo;
            }
        }
        public async Task SaveIdAsync(string userId)
        {
            await _storageService.SetSecureAsync(LoginUserKey, userId);
        }

        public async Task SavePasswordAsync(string password)
        {
            await _storageService.SetSecureAsync(LoginPasswordKey, password);
        }
        public async Task<string> GetIdAsync()
        {
            var userid = await _storageService.GetSecureOrCreateAsync<string>(LoginUserKey, null);
            return userid;
        }
        public async Task<string> GetPasswordAsync()
        {
            var password = await _storageService.GetSecureOrCreateAsync<string>(LoginPasswordKey, null);
            return password;
        }

        public void SetRaptorAccount(string accountId)
        {
            AccountId = accountId;
        }

        public void DeleteId()
        {
            _storageService.RemoveSecure(LoginUserKey);
        }

        public void DeletePassword()
        {
            _storageService.RemoveSecure(LoginPasswordKey);
        }

        public Task LogoutAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}

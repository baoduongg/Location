using System;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GetLocation.Interfaces.Storage
{
    public class StorageService : IStorageService
    {
        #region plain storage

        public Task<T> GetAsync<T>(string key)
        {
            var value = Application.Current.Properties[key];
            return Task.FromResult(Deserialize<T>(value?.ToString()));
        }

        public async Task<T> GetOrCreateAsync<T>(string key, T defaultValue)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            var contained = Application.Current.Properties.ContainsKey(key);
            if (contained)
                return await GetAsync<T>(key);
            // begin write
            await SetAsync(key, defaultValue);
            return defaultValue;
        }

        public bool Remove(string key) => Application.Current.Properties.Remove(key);

        public Task SetAsync<T>(string key, T value)
        {
            //if (value == null)
            //    throw new NullReferenceException($"Should not write a null value.");
            var toWrite = Serialize(value);
            var contained = Application.Current.Properties.ContainsKey(key);
            if (!contained)
                Application.Current.Properties.Add(key, toWrite);
            else
                Application.Current.Properties[key] = toWrite;
            return Application.Current.SavePropertiesAsync();
        }

        public bool HasKey(string key)
        {
            var contained = Application.Current.Properties.ContainsKey(key);
            return contained;
        }

        #endregion

        #region secure storage

        public async Task<T> GetSecureAsync<T>(string key)
        {
            var value = await SecureStorage.GetAsync(key);
            return Deserialize<T>(value);
        }

        public async Task<T> GetSecureOrCreateAsync<T>(string key, T defaultValue)
        {
            var value = await SecureStorage.GetAsync(key);
            //value = "true";
            if (value != null)
                return Deserialize<T>(value);
            // begin write
            await SetSecureAsync(key, defaultValue);
            return defaultValue;
        }

        public bool RemoveSecure(string key)
        {
            return SecureStorage.Remove(key);
        }

        public Task SetSecureAsync<T>(string key, T value)
        {
            //if (value == null)
            //    throw new NullReferenceException($"Should not write a null value.");
            var toWrite = Serialize(value);
            return SecureStorage.SetAsync(key, toWrite);
        }
        #endregion

        #region private methods
        private JsonSerializerSettings DefaultJsonSettings()
        {
            return new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                NullValueHandling = NullValueHandling.Include,
            };
        }

        string Serialize<T>(T data) => JsonConvert.SerializeObject(data, DefaultJsonSettings());

        T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json, DefaultJsonSettings());
        #endregion
    }
}

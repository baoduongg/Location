using System;
using System.Threading.Tasks;

namespace GetLocation.Interfaces.Storage
{
    public interface IStorageService
    {
        /// <summary>
        /// get value which stored in raw storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);
        /// <summary>
        /// get value which stored in raw storage <para/>
        /// if not yet, create it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        Task<T> GetOrCreateAsync<T>(string key, T defaultValue);
        /// <summary>
        /// set a value to raw storge
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value);

        bool HasKey(string key);

        /// <summary>
        /// remove from raw storge
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);
        /// <summary>
        /// get value from secure storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetSecureAsync<T>(string key);
        /// <summary>
        /// get value from secure storage <para/>
        /// if not yet, create it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        Task<T> GetSecureOrCreateAsync<T>(string key, T defaultValue);
        /// <summary>
        /// remove from secure storage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveSecure(string key);
        /// <summary>
        /// set a value to secure storge
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetSecureAsync<T>(string key, T value);
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace GetLocation.Interfaces.Login
{
    public interface ILoginService
    {
        string AccountId { get; }

        void SetRaptorAccount(string accountId);

        Task<LoginReponse> Login(string Username,string Password);

        Task SaveIdAsync(string userId);

        void DeleteId();

        Task SavePasswordAsync(string password);

        void DeletePassword();

        Task<string> GetIdAsync();

        Task<string> GetPasswordAsync();
        Task LogoutAsync(CancellationToken token);
    }
}

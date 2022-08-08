using TwitterWebAPI.Models;

namespace TwitterWebAPI.Data
{
    public interface IAuthenticateRepository
    {
        Task<Response<int>> RegisterAsync(UserDetails user, string password);
        Task<Response<string>> LoginAsync(string loginId, string password);
        Task<Response<string>> ForgotPasswordAsync(string username);
        Task<bool> IsUserExists(string username);
    }
}

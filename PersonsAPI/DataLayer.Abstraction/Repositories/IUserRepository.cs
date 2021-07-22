using DataLayer.Abstraction.Entityes;
using System.Threading.Tasks;

namespace DataLayer.Abstraction.Repositories
{
    public interface IUserRepository
    {
        Task<int> GetUserByLogonAsync(string login, string password);
        Task<RefreshToken> GetRefreshTokenByUserIdAsync(RefreshToken refreshToken);
        Task SetNewRefreshTokenAsync(RefreshToken refreshToken);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenByTokenIdAsync(string token);
        Task CreateNewUserAsync(string login, string password);
    }
}

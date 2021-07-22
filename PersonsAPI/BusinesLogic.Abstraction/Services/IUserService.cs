using DataLayer.Abstraction.Entityes;
using System.Threading.Tasks;

namespace BusinesLogic.Abstraction.Services
{
	public interface IUserService
	{
		Task<TokenResponse> Authentificate(string user, string password);
		Task<string> RefreshToken(string token);
        Task<int> GetUserByLogonAsync(string login, string password);
        Task CreateNewUserAsync(string login, string password);
    }
}

using DataLayer.Abstraction.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Abstraction.Services
{
	public interface IUserService
	{
		TokenResponse Authenticate(string user, string password);
		string RefreshToken(string token);
	}
}

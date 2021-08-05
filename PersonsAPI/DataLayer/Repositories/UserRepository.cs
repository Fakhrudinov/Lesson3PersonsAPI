using DataLayer.Abstraction.Entityes;
using DataLayer.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDataContext _context;

        public UserRepository(ApplicationDataContext context)
        {
            _context = context;
        }


        public async Task<int> GetUserByLogonAsync(string login, string password)
        {
            User findedUser =  (await _context.Users.Where(x => x.Login==login && x.Password == password).SingleOrDefaultAsync());

            if (findedUser == null)
                return 0;

            return findedUser.Id;
        }


        public async Task<RefreshToken> GetRefreshTokenByUserIdAsync(RefreshToken refreshToken)
        {
            return await _context.RefreshTokens.Where(x => x.UserId == refreshToken.UserId).SingleOrDefaultAsync();
        }

        public async Task SetNewRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            RefreshToken tokenToEdit = await _context.RefreshTokens.Where(x => x.UserId == refreshToken.UserId).SingleOrDefaultAsync();

            if (tokenToEdit != null)
            {
                tokenToEdit.Token = refreshToken.Token;
                tokenToEdit.UserId = refreshToken.UserId;
                tokenToEdit.Expires = refreshToken.Expires;

                _context.RefreshTokens.Update(tokenToEdit);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<RefreshToken> GetRefreshTokenByTokenIdAsync(string token)
        {
            return await _context.RefreshTokens.Where(x => x.Token == token).SingleOrDefaultAsync();
        }

        public async Task CreateNewUserAsync(string login, string password)
        {
            User newUser = new User();
            newUser.Login = login;
            newUser.Password = password;

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}

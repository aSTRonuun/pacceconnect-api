using Domain.UserDomain.Entities;
using Domain.UserDomain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.UserData
{
    public class UserRepository : IUserRepository
    {
        private readonly PACCEConnectDbContext _context;

        public UserRepository(PACCEConnectDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUserNameOrEmail(string userNameOrEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userNameOrEmail || u.Email == userNameOrEmail);
            return user;
        }
    }
}

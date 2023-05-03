using Domain.UserDomain.Entities;

namespace Domain.UserDomain.Ports
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserNameOrEmail(string userNameOrEmail);
    }
}

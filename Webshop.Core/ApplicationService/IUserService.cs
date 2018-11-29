using Webshop.Core.Entities;

namespace Webshop.Core.ApplicationService
{
    public interface IUserService
    {
        User GetWhereUsername(string username);

        bool CheckCorrectPassword(User user, LoginInput model);

        string GenerateToken(User user);

        User Create(User t);
    }
}
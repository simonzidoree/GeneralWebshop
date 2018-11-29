using System.Collections.Generic;
using Webshop.Core.Entities;

namespace Webshop.Core.ApplicationService
{
    public interface IUserService
    {
        User Create(User t);
        IEnumerable<User> GetAll();
    }
}
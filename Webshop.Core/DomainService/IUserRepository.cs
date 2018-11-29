using System.Collections.Generic;
using Webshop.Core.Entities;

namespace Webshop.Core.DomainService
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User Create(User t);
    }
}
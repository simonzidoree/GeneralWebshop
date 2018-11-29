using System.Collections.Generic;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data.RepositoriesSQL
{
    public class UserRepository : IUserRepository
    {
        private readonly WebshopContext _ctx;

        public UserRepository(WebshopContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<User> GetAll()
        {
            return _ctx.Users;
        }

        public User Create(User t)
        {
            var user = _ctx.Users.Add(t).Entity;
            _ctx.SaveChanges();
            return user;
        }
    }
}
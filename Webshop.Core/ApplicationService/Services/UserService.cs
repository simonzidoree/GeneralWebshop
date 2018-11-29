using System.Collections.Generic;
using System.Linq;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;

namespace Webshop.Core.ApplicationService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Create(User t)
        {
            return _repository.Create(t);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}
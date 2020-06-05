using FireBaseExemplos.Models;
using FireBaseExemplos.Repository;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireBaseExemplos.Services
{
    public class UserService
    {
        private readonly IUserRepository _fireBaseRepository;
        private readonly ConcurrentDictionary<string, UserFirebase> _users;

        public UserService(IUserRepository fireBaseRepository, ConcurrentDictionary<string, UserFirebase> users)
        {
            _fireBaseRepository = fireBaseRepository;
            _users = users;
        }

        public Task<IEnumerable<UserFirebase>> GetUsers(string name)
        {
            if (string.IsNullOrEmpty(name))
                return _fireBaseRepository.GetUsers();
            else
                return _fireBaseRepository.GetUsers((collectionRef) =>
                    collectionRef.WhereEqualTo("name", name));
        }

        public IEnumerable<UserFirebase> GetUsersStream(string name)
        {
            if (string.IsNullOrEmpty(name))
                return _users.Select(usr => usr.Value);
            else
                return _users.Select(usr => usr.Value).Where(usr => usr.Name == name);
        }

        public Task<UserFirebase> GetUsersById(string userId)
        {
            return _fireBaseRepository.GetUsersById(userId);
        }

        public Task<bool> CreateUser(UserFirebase user)
        {
            return _fireBaseRepository.CreateUser(user);
        }

        public Task<bool> DeleteUser(string userId)
        {
            return _fireBaseRepository.DeleteUser(userId);
        }
    }
}

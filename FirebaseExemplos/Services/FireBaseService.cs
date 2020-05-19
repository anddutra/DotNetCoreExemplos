using FireBaseExemplos.Models;
using FireBaseExemplos.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FireBaseExemplos.Services
{
    public class FireBaseService
    {
        private readonly IFireBaseRepository _fireBaseRepository;

        public FireBaseService(IFireBaseRepository fireBaseRepository)
        {
            _fireBaseRepository = fireBaseRepository;
        }

        public Task<IEnumerable<UserFirebase>> GetUsers(string name)
        {
            if (string.IsNullOrEmpty(name))
                return _fireBaseRepository.GetUsers();
            else
                return _fireBaseRepository.GetUsers((collectionRef) =>
                    collectionRef.WhereEqualTo("name", name));
        }

        public Task<UserFirebase> GetDocument(string id)
        {
            return _fireBaseRepository.GetDocument(id);
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

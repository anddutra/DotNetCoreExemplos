using FireBaseExemplos.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireBaseExemplos.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FirestoreDb _firestoreDb;
        private const string _collectionName = "users";

        public UserRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task<IEnumerable<UserFirebase>> GetUsers(Func<CollectionReference, Query> query = null)
        {
            var collectionRef = _firestoreDb.Collection(_collectionName);
            var snapshot = query != null ? await query.Invoke(collectionRef).GetSnapshotAsync() : await collectionRef.GetSnapshotAsync();
            return snapshot.Documents.AsParallel().Select(doc => doc.ConvertTo<UserFirebase>());
        }

        public async Task<UserFirebase> GetUsersById(string userId)
        {
            var snapshot = await _firestoreDb.Document($"{_collectionName}/{userId}").GetSnapshotAsync();
            return snapshot.ConvertTo<UserFirebase>();
        }

        public async Task<bool> CreateUser(UserFirebase user)
        {
            try
            {
                await _firestoreDb.Collection(_collectionName).AddAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                await _firestoreDb.Collection(_collectionName).Document(userId).DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Listen(Action<QuerySnapshot> callback, Func<CollectionReference, Query> query = null)
        {
            var collectionRef = _firestoreDb.Collection(_collectionName);
            if (query == null)
            {
                collectionRef.Listen(callback);
            }
            else
            {
                query.Invoke(collectionRef).Listen(callback);
            }
        }
    }
}

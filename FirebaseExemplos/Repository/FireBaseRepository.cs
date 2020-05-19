using FireBaseExemplos.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireBaseExemplos.Repository
{
    public class FireBaseRepository : IFireBaseRepository
    {
        private readonly FirestoreDb _firestoreDb;
        private const string _collectionName = "users";

        public FireBaseRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task<IEnumerable<UserFirebase>> GetUsers(Func<CollectionReference, Query> query = null)
        {
            var collectionRef = _firestoreDb.Collection(_collectionName);
            var snapshot = query != null ? await query.Invoke(collectionRef).GetSnapshotAsync() : await collectionRef.GetSnapshotAsync();
            return snapshot.Documents.AsParallel().Select(doc => doc.ConvertTo<UserFirebase>());
        }

        public async Task<UserFirebase> GetDocument(string id)
        {
            var snapshot = await _firestoreDb.Document($"{_collectionName}/{id}").GetSnapshotAsync();
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
    }
}

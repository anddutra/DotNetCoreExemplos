using Google.Cloud.Firestore;

namespace FireBaseExemplos.Models
{
    [FirestoreData]
    public class UserFirebase
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty("name")]
        public string Name { get; set; }
        [FirestoreProperty("lastName")]
        public string LastName { get; set; }
        [FirestoreProperty("email")]
        public string Email { get; set; }
    }
}

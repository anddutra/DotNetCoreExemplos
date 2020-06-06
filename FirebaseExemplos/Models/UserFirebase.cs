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
        [FirestoreProperty("status")]
        public string Status { get; set; }
        //public UserStatus Status { get; set; }
    }

    //[FirestoreData(ConverterType = typeof(EnumFirestoreConverter<UserStatus>))]
    //public enum UserStatus
    //{
    //    [EnumValue("A")]
    //    Active,
    //    [EnumValue("I")]
    //    Inactive
    //}
}

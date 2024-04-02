using System.Text.Json.Serialization;

namespace collectionsapi.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public List<Collection>? Collections { get; set; }
    }

}

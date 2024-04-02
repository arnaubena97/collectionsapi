using System.Text.Json.Serialization;

namespace collectionsapi.Data.Entities
{
    public class CollectionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int CollectionId { get; set; }
        [JsonIgnore]
        public Collection? Collection { get; set; }
    }
}

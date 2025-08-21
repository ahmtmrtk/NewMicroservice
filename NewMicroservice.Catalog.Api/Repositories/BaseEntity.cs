using MongoDB.Bson.Serialization.Attributes;

namespace NewMicroservice.Catalog.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("id")]
        public Guid Id { get; set; }
    }
}

using MongoDB.Bson.Serialization.Attributes;

namespace NewMicroservice.Discount.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("id")]
        public Guid Id { get; set; }
    }
}

using MongoDB.Bson;

namespace Blockchain.Data.Entities;

public class BaseEntity
{
    public ObjectId Id { get; set; }
}

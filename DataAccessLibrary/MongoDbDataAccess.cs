using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLibrary
{
  public class MongoDbDataAccess
  {
    private IMongoDatabase database;

    public MongoDbDataAccess(string daName, string connectionString)
    {
      var client = new MongoClient(connectionString);
      database = client.GetDatabase(daName);
    }

    public void InsertRecord<T>(string tableName, T record)
    {
      var collection = database.GetCollection<T>(tableName);
      collection.InsertOne(record);
    }
    public List<T> LoadRecords<T>(string tableName)
    {
      var collection = database.GetCollection<T>(tableName);
      return collection.Find(new BsonDocument()).ToList();
    }
    public T LoadRecordById<T>(string table, Guid id)
    {
      var collection = database.GetCollection<T>(table);
      var filter = Builders<T>.Filter.Eq("Id", id);

      return collection.Find(filter).First();
    }

    public void UpsertRecord<T>(string table, Guid id, T record)
    {
      var collection = database.GetCollection<T>(table);

      var result = collection.ReplaceOne(
        new BsonDocument("_id", id),
        record,
        new ReplaceOptions { IsUpsert = true }
        );
    }

    public void DeleteRecord<T>(string table, Guid id)
    {
      var collection = database.GetCollection<T>(table);
      var filter = Builders<T>.Filter.Eq("Id", id);
      collection.DeleteOne(filter);
    }

  }
}
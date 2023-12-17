using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics;

namespace DataAccessLibrary.Models
{
  public class ContactModel
  {
    [BsonId]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<EmailAddressModel> EmailAddress { get; set; } = new();
    public List<PhoneNumberModel> PhoneNumbers{ get; set; } = new();
  }
}

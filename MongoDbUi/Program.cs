using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace MongoDbUi
{
  class Program
  {
    private static MongoDbDataAccess db;
    private static readonly string tableName = "Contacts";

    static void Main(string[] args)
    {
      db = new MongoDbDataAccess("MongoContactsDb", GetConnectionString());
      //ContactModel user = new ContactModel
      //{
      //  Id = new Guid("acfff736-a7f1-42ec-9990-64dc1571c1a7"),
      //  FirstName = "Tim3",
      // LastName = "Corey3"
      //};
      //user.EmailAddress.Add(new EmailAddressModel { EmailAddress = "guilherme@crestani.com" });

      //user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumbers = "1234-5678" });
      //user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumbers = "5678-1234" });

      //CreateContact(user);
      GetAllContacts();
      //GetContactById(new Guid("acfff736-a7f1-42ec-9990-64dc1571c1a7"));

      //UpdateContactFirsName(user);
      RemovePhoneNumberFromUser("1234-5678", new Guid("b7914fc2-ffec-4b4f-a930-8dcd96faefdf"));

      Console.WriteLine("Finished processing MongoDB");
      Console.ReadLine();
    }

    public static void RemovePhoneNumberFromUser(string phoneNumber, Guid id)
    {
      var contact = db.LoadRecordById<ContactModel>(tableName, id);
      contact.PhoneNumbers = contact.PhoneNumbers.Where(x => x.PhoneNumbers != phoneNumber).ToList();

      db.UpsertRecord(tableName, id, contact);
    }

    private static void UpdateContact(ContactModel contact)
    {
      //will update ALL THE CONTACT, not just what changed...
      db.UpsertRecord<ContactModel>(tableName, contact.Id, contact);
    }

    private static void UpdateContactFirsName(ContactModel newContact)
    {
      var contact = db.LoadRecordById<ContactModel>(tableName, newContact.Id);
      contact.FirstName = newContact.FirstName;
      db.UpsertRecord(tableName, contact.Id, contact);

    }

    private static void GetContactById(Guid id)
    {
      var contact = db.LoadRecordById<ContactModel>(tableName, id);
      PrintContact(contact);
    }

    private static void CreateContact(ContactModel contact)
    {
      db.UpsertRecord(tableName, contact.Id, contact);
    }

    private static void GetAllContacts()
    {
      var contacts = db.LoadRecords<ContactModel>(tableName);

      foreach (var contact in contacts)
      {
        PrintContact(contact);
      }
    }

    private static void PrintContact(ContactModel contact)
    {
      Console.WriteLine($"Contact: \n{contact.Id}\n{contact.FirstName}\n{contact.LastName}");
      foreach (var phone in contact.PhoneNumbers)
      {
        Console.WriteLine($"  {phone.PhoneNumbers}");
      }
      foreach (var mail in contact.EmailAddress)
      {
        Console.WriteLine($"  {mail.EmailAddress}");
      }
      Console.WriteLine("\n-------");
    }

    private static string GetConnectionString(string connectionStringName = "Default")
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");
      var config = builder.Build();
      return config.GetConnectionString(connectionStringName);
    }
  }
}

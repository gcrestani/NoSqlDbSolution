using Microsoft.Extensions.Configuration;

namespace MongoDbUi
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Finished processing MongoDB");
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



/*private static string GetConnectionString(string connectionStringName = "Default")
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");
      var config = builder.Build();
      
      string output = config.GetConnectionString(connectionStringName);

      return output;
    }*/
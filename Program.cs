// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;
using System.Threading.Tasks;

// connection string to your Redis Cache    

public class Program
{
 
  static async Task Main(string[] args)
{

        IDatabase db = RedisStore.RedisCache;
        string test_key;

        // Snippet below executes a PING to test the server connection
        var result = await db.ExecuteAsync("ping");
        Console.WriteLine($"PING = {result.Type} : {result}");
        DateTime startTime = DateTime.UtcNow;
        // Call StringSetAsync on the IDatabase object to set the key "test:key" to the value "100"
        for (int i = 1; i < 10000; i = i + 1) {
            test_key ="test:key" + i;
            bool setValue = await db.StringSetAsync(test_key, "bbbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            //Console.WriteLine($"SET: {setValue}");
        }

	Console.WriteLine("Total seconds:" + (DateTime.UtcNow - startTime).TotalSeconds);
        // StringGetAsync takes the key to retrieve and return the value
        string getValue = await db.StringGetAsync( "test:key99");
        Console.WriteLine($"GET: {getValue}");
  }
}


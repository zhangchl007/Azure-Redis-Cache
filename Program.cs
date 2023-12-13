// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;
using System.Threading.Tasks;

// connection string to your Redis Cache    

public class Program
{
 
  static SemaphoreSlim semaphore = new SemaphoreSlim(10); // 10 is the maximum number of concurrent threads
  static async Task Main(string[] args)
{

        IDatabase db = RedisStore.RedisCache;
        string test_key;

        // Snippet below executes a PING to test the server connection
        var result = await db.ExecuteAsync("ping");
        Console.WriteLine($"PING = {result.Resp2Type} : {result}");
        DateTime startTime = DateTime.UtcNow;

        List<Task> tasks = new List<Task>();

        // Call StringSetAsync on the IDatabase object to set the key "test:key" to the value "100"
        for (int i = 1; i < 10000; i = i + 1) {
            test_key ="test:key" + i;
            tasks.Add(Task.Run(async () => {
                await semaphore.WaitAsync(); // wait for a free slot
                try {
                    //string getValue = await db.StringGetAsync(test_key);
                    // Console.WriteLine($"GET: key: {test_key} value: {getValue}");
                    bool setValue = await db.StringSetAsync(test_key, "bbbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                    Console.WriteLine($"SET: {setValue}");
                }  catch (Exception ex) {
                    Console.WriteLine($"Error: {ex.Message}");
                }finally {
                    semaphore.Release(); // release the slot for other threads
                }
            }));
        }

        try {
          await Task.WhenAll(tasks).ConfigureAwait(false);
        } catch (Exception ex) {
          Console.WriteLine($"Error: {ex.Message}");
        } finally {
          // Close the connection to Redis after all tasks are complete
          db.Multiplexer.Dispose();
        }
        Console.WriteLine("Total seconds:" + (DateTime.UtcNow - startTime).TotalSeconds);
        // StringGetAsync takes the key to retrieve and return the value
        //string getValue = await db.StringGetAsync( "test:key99");
        //Console.WriteLine($"GET: {getValue}");

  }
}


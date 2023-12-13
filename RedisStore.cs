using StackExchange.Redis;
public class RedisStore
    {
        //static string connectionString = "10.1.0.7:6379,password=new.mypass,ssl=false,abortConnect=False";
        static string connectionString = "xxxx,ssl=True,abortConnect=False";
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisStore()
        {

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }

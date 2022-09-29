using StackExchange.Redis;
public class RedisStore
    {
        static string connectionString = "az204redis22271.redis.cache.windows.net:6380,password=L8ZVdaWqC9yITIyF7x5CsjrYMDL6flzaCAzCaK5zyds=,ssl=True,abortConnect=False";
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisStore()
        {

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }
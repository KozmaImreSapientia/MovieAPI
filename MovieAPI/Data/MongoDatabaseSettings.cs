namespace MovieAPI.Data
{
    public class MongoDatabaseSettings
    {
        public string ConnectionString { get; set; } 
        public string DatabaseName { get; set; }
        public string MovieLogsCollectionName { get; set; }
    }
}

namespace LibraryManagementSystem
{
    public class AppSettings
    {

        public string DatabaseHostname { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseInitialCatalog { get; set; }
        public string BooksTableName { get; set; }
        public string DvdsTableName { get; set; }

        public Models.ConnectionString DatabaseConnectionString => new(
            DatabaseHostname,
            DatabaseUsername,
            DatabasePassword,
            DatabaseInitialCatalog
        );

    }
}

namespace LibraryManagementSystem.Models
{
    public class ConnectionString
    {

        public string DatabaseHostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string InitialCatalog { get; set; }

        public ConnectionString(
            string hostname,
            string username,
            string password,
            string initialCatalog
        )
        {
            DatabaseHostname = hostname;
            Username = username;
            Password = password;
            InitialCatalog = initialCatalog;
        }

        public override string ToString()
            => $"server={DatabaseHostname};uid={Username};pwd={Password};database={InitialCatalog}";

    }
}

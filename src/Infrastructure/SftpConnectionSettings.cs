namespace File_Wizard.Infrastructure
{
    public sealed class SftpConnectionSettings
    {
        public string Host { get; }

        public int Port { get; }

        public string Username { get; }

        public string Password { get; }

        public string EnvironmentName { get; }

        public SftpConnectionSettings(string host, int port, string username, string password, string environmentName)
        {
            Host = host;
            Port = port;
            Username = username;
            Password = password;
            EnvironmentName = environmentName;
        }
    }
}
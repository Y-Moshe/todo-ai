namespace API.Helpers;

public class AWS
{
    public static string GetRDSConnectionString(
        IConfiguration config, string dbName)
    {
        if (string.IsNullOrEmpty(config["RDS_DB_NAME"])) return null;

        string username = config["RDS_USERNAME"];
        string password = config["RDS_PASSWORD"];
        string hostname = config["RDS_HOSTNAME"];
        string port = config["RDS_PORT"];

        return $@"Server={hostname}; Database={dbName}; 
                Uid={username}; Pwd={password}; Port={port}";
    }
}

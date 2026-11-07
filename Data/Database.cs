using Npgsql;

namespace ClinicQueue.Data;

public class Database
{
    private static string connectionString = "Host=localhost;Port=5432;Database=clinicqueue;Username=postgres;Password=1234";

    public static NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(connectionString);
    }
}
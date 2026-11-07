
using Npgsql;
using ClinicQueue.Data;
using ClinicQueue.Models;
using Dapper;

namespace ClinicQueue.Repositories;

public class PatientRepository
{
    public async Task<List<Patient>> GetAll()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select * from patients order by id";

        await using var command = new NpgsqlCommand(query, connection);

        await using var reader = await command.ExecuteReaderAsync();

        var patients = new List<Patient>();

        while (await reader.ReadAsync())
        {
            patients.Add(new Patient
            {
                Id = reader.GetInt32(0),
                Fullname = reader.GetString(1),
                Phone = reader.GetString(2),
                Birthdate = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                IsActive = reader.GetBoolean(4),
                CreatedAt = reader.GetDateTime(5)
            });
        }

        return patients;
    }
    public async Task<Patient?> GetById(int id)
{
    await using var connection = Database.Connection();
    await connection.OpenAsync();

    var query = "select * from patients where id = @id";

    await using var command = new NpgsqlCommand(query, connection);
    command.Parameters.AddWithValue("@id", id);

    await using var reader = await command.ExecuteReaderAsync();

    if (await reader.ReadAsync())
    {
        return new Patient
        {
            Id = reader.GetInt32(0),
            Fullname = reader.GetString(1),
            Phone = reader.GetString(2),
            Birthdate = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
            IsActive = reader.GetBoolean(4),
            CreatedAt = reader.GetDateTime(5)
        };
    }

    return null;
}

public async Task<int> Add(Patient patient)
{
    await using var connection = Database.Connection();
    await connection.OpenAsync();

    var query = """
        insert into patients (fullname, phone, birthdate)
        values (@Fullname, @Phone, @Birthdate)
        returning id
        """;

    return await connection.ExecuteScalarAsync<int>(query, patient);
}

public async Task<bool> Update(int id, Patient patient)
{
    await using var connection = Database.Connection();
    await connection.OpenAsync();

    var query = """
        update patients
        set fullname = @Fullname,
            phone = @Phone,
            birthdate = @Birthdate
        where id = @Id
        """;

    patient.Id = id;

    var rows = await connection.ExecuteAsync(query, patient);
    return rows > 0;
}

public async Task<bool> Deactivate(int id)
{
    await using var connection = Database.Connection();
    await connection.OpenAsync();

    var query = """
        update patients
        set isactive = false
        where id = @id
        """;

    var rows = await connection.ExecuteAsync(query, new { id });

    return rows > 0;
}
}

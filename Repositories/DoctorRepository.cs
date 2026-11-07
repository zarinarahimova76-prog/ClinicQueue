
using Dapper;
using ClinicQueue.Data;
using ClinicQueue.Models;

namespace ClinicQueue.Repositories;

public class DoctorRepository
{
    public async Task<List<Doctor>> GetAll()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select * from doctors";

        var result = await connection.QueryAsync<Doctor>(query);

        return result.ToList();
    }

    public async Task<Doctor?> GetById(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select * from doctors where id = @id";

        return await connection.QueryFirstOrDefaultAsync<Doctor>(query, new { id });
    }

    public async Task<int> Add(Doctor doctor)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = """
            insert into doctors (fullname, specialty, hiredat)
            values (@Fullname, @Specialty, @HiredAt)
            returning id
            """;

        return await connection.ExecuteScalarAsync<int>(query, doctor);
    }

    public async Task<bool> Update(int id, Doctor doctor)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = """
            update doctors
            set fullname = @Fullname,
                specialty = @Specialty,
                hiredat = @HiredAt
            where id = @Id
            """;

        doctor.Id = id;

        var rows = await connection.ExecuteAsync(query, doctor);

        return rows > 0;
    }

    public async Task<bool> Deactivate(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "update doctors set isactive = false where id = @id";

        var rows = await connection.ExecuteAsync(query, new { id });

        return rows > 0;
    }
}

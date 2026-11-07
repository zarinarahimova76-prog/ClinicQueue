
using Dapper;
using ClinicQueue.Data;
using ClinicQueue.Models;

namespace ClinicQueue.Repositories;

public class RoomRepository
{
    public async Task<List<Room>> GetAll()
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "select * from rooms";

        var result = await connection.QueryAsync<Room>(query);

        return result.ToList();
    }

    public async Task<int> Add(Room room)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = """
            insert into rooms (name)
            values (@Name)
            returning id
            """;

        return await connection.ExecuteScalarAsync<int>(query, room);
    }

    public async Task<bool> Update(int id, Room room)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = """
            update rooms
            set name = @Name
            where id = @Id
            """;

        room.Id = id;

        var rows = await connection.ExecuteAsync(query, room);

        return rows > 0;
    }

    public async Task<bool> Deactivate(int id)
    {
        await using var connection = Database.Connection();
        await connection.OpenAsync();

        var query = "update rooms set isactive = false where id = @id";

        var rows = await connection.ExecuteAsync(query, new { id });

        return rows > 0;
    }
}

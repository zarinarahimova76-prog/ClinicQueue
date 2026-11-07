
using ClinicQueue.Models;
using ClinicQueue.Repositories;
using Domain.Models;

namespace ClinicQueue.Services;

public class RoomService
{
    private readonly RoomRepository _repository;

    public RoomService()
    {
        _repository = new RoomRepository();
    }

    public async Task<ApiResponse<List<Room>>> GetAll()
    {
        var rooms = await _repository.GetAll();

        return new ApiResponse<List<Room>>
        {
            StatusCode = 200,
            Message = "Rooms retrieved successfully",
            Data = rooms
        };
    }

    public async Task<ApiResponse<int>> Add(Room room)
    {
        var id = await _repository.Add(room);

        return new ApiResponse<int>
        {
            StatusCode = 201,
            Message = "Room created successfully",
            Data = id
        };
    }

    public async Task<ApiResponse<bool>> Update(int id, Room room)
    {
        var result = await _repository.Update(id, room);

        return new ApiResponse<bool>
        {
            StatusCode = result ? 200 : 404,
            Message = result ? "Room updated successfully" : "Room not found",
            Data = result
        };
    }

    public async Task<ApiResponse<bool>> Deactivate(int id)
    {
        var result = await _repository.Deactivate(id);

        return new ApiResponse<bool>
        {
            StatusCode = result ? 200 : 404,
            Message = result ? "Room deactivated successfully" : "Room not found",
            Data = result
        };
    }
}

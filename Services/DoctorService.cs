
using ClinicQueue.Models;
using ClinicQueue.Repositories;
using Domain.Models;

namespace ClinicQueue.Services;

public class DoctorService
{
    private readonly DoctorRepository _repository;

    public DoctorService()
    {
        _repository = new DoctorRepository();
    }

    public async Task<ApiResponse<List<Doctor>>> GetAll()
    {
        var doctors = await _repository.GetAll();

        return new ApiResponse<List<Doctor>>
        {
            StatusCode = 200,
            Message = "Doctors retrieved successfully",
            Data = doctors
        };
    }

    public async Task<ApiResponse<Doctor>> GetById(int id)
    {
        var doctor = await _repository.GetById(id);

        if (doctor == null)
        {
            return new ApiResponse<Doctor>
            {
                StatusCode = 404,
                Message = "Doctor not found",
                Data = null
            };
        }

        return new ApiResponse<Doctor>
        {
            StatusCode = 200,
            Message = "Doctor retrieved successfully",
            Data = doctor
        };
    }

    public async Task<ApiResponse<int>> Add(Doctor doctor)
    {
        var id = await _repository.Add(doctor);

        return new ApiResponse<int>
        {
            StatusCode = 201,
            Message = "Doctor created successfully",
            Data = id
        };
    }

    public async Task<ApiResponse<bool>> Update(int id, Doctor doctor)
    {
        var result = await _repository.Update(id, doctor);

        return new ApiResponse<bool>
        {
            StatusCode = result ? 200 : 404,
            Message = result ? "Doctor updated successfully" : "Doctor not found",
            Data = result
        };
    }

    public async Task<ApiResponse<bool>> Deactivate(int id)
    {
        var result = await _repository.Deactivate(id);

        return new ApiResponse<bool>
        {
            StatusCode = result ? 200 : 404,
            Message = result ? "Doctor deactivated successfully" : "Doctor not found",
            Data = result
        };
    }
}

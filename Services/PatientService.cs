using ClinicQueue.Models;
using ClinicQueue.Repositories;
using Domain.Models;

namespace ClinicQueue.Services;

public class PatientService
{
    private readonly PatientRepository _repository;

    public PatientService()
    {
        _repository = new PatientRepository();
    }

    public async Task<ApiResponse<List<Patient>>> GetAll()
    {
        var patients = await _repository.GetAll();

        return new ApiResponse<List<Patient>>
        {
            StatusCode = 200,
            Message = "Patients retrieved successfully",
            Data = patients
        };
    }
    public async Task<ApiResponse<Patient>> GetById(int id)
{
    var patient = await _repository.GetById(id);

    if (patient == null)
    {
        return new ApiResponse<Patient>
        {
            StatusCode = 404,
            Message = "Patient not found",
            Data = null
        };
    }

    return new ApiResponse<Patient>
    {
        StatusCode = 200,
        Message = "Patient retrieved successfully",
        Data = patient
    };
}

public async Task<ApiResponse<int>> Add(Patient patient)
{
    var id = await _repository.Add(patient);

    return new ApiResponse<int>
    {
        StatusCode = 201,
        Message = "Patient created successfully",
        Data = id
    };
}

public async Task<ApiResponse<bool>> Update(int id, Patient patient)
{
    var result = await _repository.Update(id, patient);

    if (!result)
    {
        return new ApiResponse<bool>
        {
            StatusCode = 404,
            Message = "Patient not found",
            Data = false
        };
    }

    return new ApiResponse<bool>
    {
        StatusCode = 200,
        Message = "Patient updated successfully",
        Data = true
    };
}

 public async Task<ApiResponse<bool>> Deactivate(int id)
{
    var result = await _repository.Deactivate(id);

    if (!result)
    {
        return new ApiResponse<bool>
        {
            StatusCode = 404,
            Message = "Patient not found",
            Data = false
        };
    }

    return new ApiResponse<bool>
    {
        StatusCode = 200,
        Message = "Patient deactivated successfully",
        Data = true
    };
}
}
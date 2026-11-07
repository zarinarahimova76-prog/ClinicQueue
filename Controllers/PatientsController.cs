using Microsoft.AspNetCore.Mvc;
using ClinicQueue.Models;
using ClinicQueue.Services;

namespace ClinicQueue.Controllers;

[ApiController]
[Route("patients")]
public class PatientsController : ControllerBase
{
    private readonly PatientService _service;

    public PatientsController()
    {
        _service = new PatientService();
    }

    [HttpGet]
public async Task<IActionResult> GetAll()
{
    var response = await _service.GetAll();

    return StatusCode(response.StatusCode, response);
}

[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var response = await _service.GetById(id);

    return StatusCode(response.StatusCode, response);
}

[HttpPost]
public async Task<IActionResult> Add(Patient patient)
{
    var response = await _service.Add(patient);

    return StatusCode(response.StatusCode, response);
}

[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, Patient patient)
{
    var response = await _service.Update(id, patient);

    return StatusCode(response.StatusCode, response);
}

[HttpPatch("{id}/deactivate")]
public async Task<IActionResult> Deactivate(int id)
{
    var response = await _service.Deactivate(id);

    return StatusCode(response.StatusCode, response);
}

}

using Microsoft.AspNetCore.Mvc;
using ClinicQueue.Models;
using ClinicQueue.Services;

namespace ClinicQueue.Controllers;

[ApiController]
[Route("doctors")]
public class DoctorsController : ControllerBase
{
    private readonly DoctorService _service;

    public DoctorsController()
    {
        _service = new DoctorService();
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
    public async Task<IActionResult> Add(Doctor doctor)
    {
        var response = await _service.Add(doctor);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Doctor doctor)
    {
        var response = await _service.Update(id, doctor);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var response = await _service.Deactivate(id);
        return StatusCode(response.StatusCode, response);
    }
}

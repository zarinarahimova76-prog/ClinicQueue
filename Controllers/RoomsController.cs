
using Microsoft.AspNetCore.Mvc;
using ClinicQueue.Models;
using ClinicQueue.Services;

namespace ClinicQueue.Controllers;

[ApiController]
[Route("rooms")]
public class RoomsController : ControllerBase
{
    private readonly RoomService _service;

    public RoomsController()
    {
        _service = new RoomService();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.GetAll();
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Room room)
    {
        var response = await _service.Add(room);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Room room)
    {
        var response = await _service.Update(id, room);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var response = await _service.Deactivate(id);
        return StatusCode(response.StatusCode, response);
    }
}

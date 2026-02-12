using AppointmentService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private static readonly List<Appointment> Appointments = new();

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Appointments);
    }

    [HttpPost]
    public IActionResult Create(Appointment appointment)
    {
        appointment.Id = Guid.NewGuid();
        Appointments.Add(appointment);
        return CreatedAtAction(nameof(GetAll), appointment);
    }
}

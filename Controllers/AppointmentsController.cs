#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentAppBackend;
using AppointmentAppBackend.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace AppointmentAppBackend.Controllers;

[Authorize]
[Route("/appointments")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly AppointmentContext _context;

    public AppointmentsController(AppointmentContext context)
    {
        _context = context;
    }

    // GET: api/Appointments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        var userId = Int32.Parse(HttpContext.User.Claims.Where(c => 
            c.Type == ClaimTypes.NameIdentifier).First().Value);

        return await _context.Appointments.Where(x => x.UserId == userId).ToListAsync();
    }

    // GET: api/Appointments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        return appointment;
    }

    // PUT: api/Appointments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
    {

        if (id != appointment.Id)
        {
            return BadRequest();
        }

        
        _context.Entry(appointment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AppointmentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Appointments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
    {
        var userId = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

        appointment.UserId = Int32.Parse(userId);

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
    }

    // DELETE: api/Appointments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AppointmentExists(int id)
    {
        return _context.Appointments.Any(e => e.Id == id);
    }
}

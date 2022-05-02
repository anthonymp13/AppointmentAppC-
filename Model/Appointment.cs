using System.Xml.Linq;
using System.Xml;
using System;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentAppBackend;

public class Appointment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? LastChanged { get; set; }
    public DateTime Start { get; set; }
    public TimeSpan? Duration { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}

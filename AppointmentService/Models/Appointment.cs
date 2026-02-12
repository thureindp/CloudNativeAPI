namespace AppointmentService.Models;
public class Appointment
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public DateTime AppointmentTime { get; set; }
    public string Status { get; set; } = "Scheduled";
}

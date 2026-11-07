namespace ClinicQueue.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string Specialty { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime HiredAt { get; set; }
}
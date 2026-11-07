namespace ClinicQueue.Models;

public class Patient
{
    public int Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime? Birthdate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
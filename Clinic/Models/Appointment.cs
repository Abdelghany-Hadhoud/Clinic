namespace Clinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}

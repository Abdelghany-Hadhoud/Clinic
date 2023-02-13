using MediatR;

namespace Clinic.Resources.Commands
{
    public class UpdateAppointmentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public DateTime AppointmentDate { get; set; }

        public UpdateAppointmentCommand(int id, string name, string phoneNumber, string note, DateTime appointmentDate)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Note = note;
            AppointmentDate = appointmentDate;
        }
    }
}

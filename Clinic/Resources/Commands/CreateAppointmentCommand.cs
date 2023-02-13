using Clinic.Models;
using MediatR;

namespace Clinic.Resources.Commands
{
    public class CreateAppointmentCommand : IRequest<Appointment>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public DateTime AppointmentDate { get; set; }

        public CreateAppointmentCommand(string name, string phoneNumber, string note, DateTime appointmentDate)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Note = note;
            AppointmentDate = appointmentDate;
        }
    }
}

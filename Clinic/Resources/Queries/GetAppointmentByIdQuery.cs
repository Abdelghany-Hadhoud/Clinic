using Clinic.Models;
using MediatR;

namespace Clinic.Resources.Queries
{
    public class GetAppointmentByIdQuery : IRequest<Appointment>
    {
        public int Id { get; set; }
    }
}

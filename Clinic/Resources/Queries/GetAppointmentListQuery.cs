using Clinic.Models;
using MediatR;

namespace Clinic.Resources.Queries
{
    public class GetAppointmentListQuery : IRequest<List<Appointment>>
    {
    }
}

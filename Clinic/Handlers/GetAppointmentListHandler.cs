using Clinic.Models;
using Clinic.Repositories;
using Clinic.Resources.Queries;
using MediatR;

namespace Clinic.Handlers
{
    public class GetAppointmentListHandler : IRequestHandler<GetAppointmentListQuery, List<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentListHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<Appointment>> Handle(GetAppointmentListQuery query, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetAppointmentListAsync();
        }
    }
}

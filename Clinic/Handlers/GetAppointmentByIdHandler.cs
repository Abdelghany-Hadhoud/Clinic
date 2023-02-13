using Clinic.Models;
using Clinic.Repositories;
using Clinic.Resources.Queries;
using MediatR;

namespace Clinic.Handlers
{
    public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentByIdHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> Handle(GetAppointmentByIdQuery query, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(query.Id);
        }
    }
}

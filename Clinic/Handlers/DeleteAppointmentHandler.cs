using Clinic.Repositories;
using Clinic.Resources.Commands;
using MediatR;

namespace Clinic.Handlers
{
    public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, int>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public DeleteAppointmentHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<int> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(command.Id);
            if (appointment == null)
                return default;

            return await _appointmentRepository.DeleteAppointmentAsync(appointment.Id);
        }
    }
}

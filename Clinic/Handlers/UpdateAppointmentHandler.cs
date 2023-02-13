using Clinic.Repositories;
using Clinic.Resources.Commands;
using MediatR;

namespace Clinic.Handlers
{
    public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointmentCommand, int>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<int> Handle(UpdateAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(command.Id);
            if (appointment == null)
                return default;

            var appointments = await _appointmentRepository.GetAppointmentsByDateAsync(command.AppointmentDate, command.Id);
            if (appointments.Any() && appointments.Count > 1)
                return default;

            appointment.Name = command.Name;
            appointment.PhoneNumber = command.PhoneNumber;
            appointment.Note = command.Note;
            appointment.AppointmentDate = command.AppointmentDate;

            return await _appointmentRepository.UpdateAppointmentAsync(appointment);
        }
    }
}

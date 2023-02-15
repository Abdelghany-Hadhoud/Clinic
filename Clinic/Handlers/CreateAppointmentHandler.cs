using Clinic.Models;
using Clinic.Repositories;
using Clinic.Resources.Commands;
using MediatR;

namespace Clinic.Handlers
{
    public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CreateAppointmentHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Appointment> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDateAsync(command.AppointmentDate, null);
            if (appointments != null && appointments.Any() && appointments.Count > 1)
                return default;

            var appointment = new Appointment()
            {
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
                Note = command.Note,
                AppointmentDate = command.AppointmentDate
            };

            return await _appointmentRepository.AddAppointmentAsync(appointment);
        }
    }
}

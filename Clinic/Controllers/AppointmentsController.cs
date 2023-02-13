using Clinic.Models;
using Clinic.Resources.Commands;
using Clinic.Resources.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AppointmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Appointment>> GetAppointmentListAsync()
        {
            var appointment = await mediator.Send(new GetAppointmentListQuery());

            return appointment;
        }

        [HttpGet("appointmentId")]
        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await mediator.Send(new GetAppointmentByIdQuery() { Id = appointmentId });

            return appointment;
        }

        [HttpPost]
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            var appointmentEntity = await mediator.Send(new CreateAppointmentCommand(
                appointment.Name,
                appointment.PhoneNumber,
                appointment.Note,
                appointment.AppointmentDate));
            return appointmentEntity;
        }

        [HttpPut]
        public async Task<int> UpdateAppointmentAsync(Appointment appointment)
        {
            var isAppointmentUpdated = await mediator.Send(new UpdateAppointmentCommand(
               appointment.Id,
               appointment.Name,
               appointment.PhoneNumber,
               appointment.Note,
               appointment.AppointmentDate));
            return isAppointmentUpdated;
        }

        [HttpDelete]
        public async Task<int> DeleteAppointmentAsync(int Id)
        {
            return await mediator.Send(new DeleteAppointmentCommand() { Id = Id });
        }
    }
}

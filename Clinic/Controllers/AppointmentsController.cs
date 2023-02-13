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
            try
            {
                var appointments = await mediator.Send(new GetAppointmentListQuery());
                return appointments;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("appointmentId")]
        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            try
            {
                var appointment = await mediator.Send(new GetAppointmentByIdQuery() { Id = appointmentId });

                return appointment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            try
            {

                var appointmentEntity = await mediator.Send(new CreateAppointmentCommand(
                    appointment.Name,
                    appointment.PhoneNumber,
                    appointment.Note,
                    appointment.AppointmentDate));
                return appointmentEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPut]
        public async Task<int> UpdateAppointmentAsync(Appointment appointment)
        {
            try
            {

                var isAppointmentUpdated = await mediator.Send(new UpdateAppointmentCommand(
                   appointment.Id,
                   appointment.Name,
                   appointment.PhoneNumber,
                   appointment.Note,
                   appointment.AppointmentDate));
                return isAppointmentUpdated;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        [HttpDelete]
        public async Task<int> DeleteAppointmentAsync(int Id)
        {
            try
            {
                return await mediator.Send(new DeleteAppointmentCommand() { Id = Id });
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}

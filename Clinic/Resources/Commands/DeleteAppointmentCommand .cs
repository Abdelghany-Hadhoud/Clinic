using MediatR;

namespace Clinic.Resources.Commands
{
    public class DeleteAppointmentCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

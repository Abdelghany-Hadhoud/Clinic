using Clinic.Models;

namespace Clinic.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<int> DeleteAppointmentAsync(int Id);
        Task<Appointment> GetAppointmentByIdAsync(int Id);
        Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime appointmentDate, int? id);
        Task<List<Appointment>> GetAppointmentListAsync();
        Task<int> UpdateAppointmentAsync(Appointment appointment);
    }
}

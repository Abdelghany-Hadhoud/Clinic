using Clinic.Data;
using Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DbContextClass _dbContext;

        public AppointmentRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            var result = _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> DeleteAppointmentAsync(int Id)
        {
            var filteredData = _dbContext.Appointments.Where(x => x.Id == Id).FirstOrDefault();
            _dbContext.Appointments.Remove(filteredData);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int Id)
        {
            return await _dbContext.Appointments.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime appointmentDate, int? id)
        {
            return await _dbContext.Appointments.Where(x => x.AppointmentDate == appointmentDate && (id == null || x.Id != id)).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentListAsync()
        {
            return await _dbContext.Appointments.ToListAsync();
        }

        public async Task<int> UpdateAppointmentAsync(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            return await _dbContext.SaveChangesAsync();
        }
    }
}

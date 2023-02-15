using Clinic.Controllers;
using Clinic.Data;
using Clinic.Handlers;
using Clinic.Models;
using Clinic.Repositories;
using Clinic.Resources.Commands;
using Clinic.Resources.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.MSUnitTests
{
    [TestClass]
    public class AppointmentTest
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly Mock<IConfiguration> mockConfiguration;
        public AppointmentTest()
        {
            mockConfiguration = new Mock<IConfiguration>();
            var options = GetMemoryTestOptions();
            SetTestData(options);
            var context = new DbContextClass(mockConfiguration.Object, options, true);
            _appointmentRepository = new AppointmentRepository(context);
        }
        [TestMethod]
        public async Task GetAppointmentListAsync_ShouldReturnThreeAppointmentList()
        {
            //Arrange
            var query = new GetAppointmentListQuery();
            var handler = new GetAppointmentListHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 3);
        }
        [TestMethod]
        public async Task GetAppointmentById_ShouldReturnAppointment_WhenAppointmentExists()
        {
            //Arrange
            var appointmentId = 1;
            var query = new GetAppointmentByIdQuery() { Id = appointmentId };
            var handler = new GetAppointmentByIdHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(appointmentId, result.Id);
            Assert.IsTrue(result.Name == "Ahmed Ali");
        }
        [TestMethod]
        public async Task GetAppointmentById_ShouldReturnNothing_WhenAppointmentDoesNotExist()
        {
            //Arrange
            var appointmentId = 5;
            var query = new GetAppointmentByIdQuery() { Id = appointmentId };
            var handler = new GetAppointmentByIdHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task AddAppointment_ShouldReturnAppointment_WhenAppointmentNotConflict()
        {
            //Arrange
            var name = "Mohamed Khaled";
            var command = new CreateAppointmentCommand(name, "0529935434", "Nothing", new DateTime(2023, 8, 4, 10, 15, 1));
            var handler = new CreateAppointmentHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == name);
        }
        [TestMethod]
        public async Task AddAppointment_ShouldReturnNothing_WhenAppointmentConflictInDate()
        {
            //Arrange
            var sameAppointmentDate = new DateTime(2023, 8, 1, 12, 1, 1);
            var command = new CreateAppointmentCommand("Waleed Khaled", "0523995434", "Nothing", sameAppointmentDate);
            var handler = new CreateAppointmentHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task UpdateAppointment_ShouldReturnTrue_WhenAppointmentExistAndNotConflict()
        {
            //Arrange
            var command = new UpdateAppointmentCommand(3, "Mohamed Kkaled", "0529935434", "Nothing", new DateTime(2023, 8, 6, 9, 15, 1));
            var handler = new UpdateAppointmentHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsTrue(result == 1);
        }
        [TestMethod]
        public async Task UpdateAppointment_ShouldReturnFalse_WhenAppointmentExistButConflictsInDate()
        {
            //Arrange
            var sameAppointmentDate = new DateTime(2023, 8, 1, 12, 1, 1);
            var command = new UpdateAppointmentCommand(3, "Mohamed Kkaled", "0529935434", "Nothing", sameAppointmentDate);
            var handler = new UpdateAppointmentHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsTrue(result == 0);
        }
        [TestMethod]
        public async Task UpdateAppointment_ShouldReturnFalse_WhenAppointmentNotExist()
        {
            //Arrange
            var appointmentId = 4;
            var command = new UpdateAppointmentCommand(appointmentId, "Mohamed Kkaled", "0529935434", "Nothing", new DateTime(2023, 8, 6, 9, 15, 1));
            var handler = new UpdateAppointmentHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsTrue(result == 0);
        }
        [TestMethod]
        public async Task DeleteAppointment_ShouldReturnTrue_WhenAppointmentExist()
        {
            //Arrange
            var appointmentId = 1;
            var command = new DeleteAppointmentCommand() { Id = appointmentId };
            var handler = new DeleteAppointmentHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsTrue(result == 1);
        }
        [TestMethod]
        public async Task DeleteAppointment_ShouldReturnFalse_WhenAppointmentNotExist()
        {
            //Arrange
            var appointmentId = 4;
            var command = new DeleteAppointmentCommand() { Id = appointmentId };
            var handler = new DeleteAppointmentHandler(_appointmentRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsTrue(result == 0);
        }
        private void SetTestData(DbContextOptions options)
        {
            // Insert seed data into the database using one instance of the context
            var sameAppointmentDate = new DateTime(2023, 8, 1, 12, 1, 1);
            using (var context = new DbContextClass(mockConfiguration.Object, options, true))
            {
                //Reset Data for new test
                context.Appointments.RemoveRange(context.Appointments);
                context.SaveChanges();
                //Mock Test Data
                context.Appointments.AddRange(new Appointment { Id = 1, Name = "Ahmed Ali", PhoneNumber = "0523435434", Note = "Nothing", AppointmentDate = sameAppointmentDate },
                new Appointment { Id = 2, Name = "Mohamed Yasser", PhoneNumber = "0534343323", Note = "Nothing", AppointmentDate = sameAppointmentDate },
                new Appointment { Id = 3, Name = "Adel Khaled", PhoneNumber = "0599884340", Note = "Nothing", AppointmentDate = new DateTime(2023, 8, 3, 11, 15, 1) });
                context.SaveChanges();
            }
        }
        private DbContextOptions GetMemoryTestOptions()
        {
            var options = new DbContextOptionsBuilder<DbContextClass>()
                             .UseInMemoryDatabase(databaseName: "ClinicDatabase")
                             .Options;
            return options;
        }
    }
}

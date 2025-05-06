using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalAppointmentsAPI.Application.Services;
using MedicalAppointmentsAPI.Application.Interfaces;
using MedicalAppointmentsAPI.Domain.Entities;
using MedicalAppointmentsAPI.Application.DTOs;
using System;

public class AppointmentServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAppointments()
    {
        var mockRepo = new Mock<IAppointmentRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Appointment> {
                new Appointment { Id = 1, DateTime = DateTime.Now, Motive = "Consulta" }
            });

        mockMapper.Setup(mapper => mapper.Map<IEnumerable<AppointmentDTO>>(It.IsAny<IEnumerable<Appointment>>()))
            .Returns(new List<AppointmentDTO> {
                new AppointmentDTO { Id = 1, Motive = "Consulta" }
            });

        var service = new AppointmentService(mockRepo.Object, mockMapper.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("Consulta", result.First().Motive);
    }
}
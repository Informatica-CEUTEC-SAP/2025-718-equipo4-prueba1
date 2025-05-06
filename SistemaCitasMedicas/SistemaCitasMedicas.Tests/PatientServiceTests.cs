using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalAppointmentsAPI.Application.Services;
using MedicalAppointmentsAPI.Application.Interfaces;
using MedicalAppointmentsAPI.Domain.Entities;
using MedicalAppointmentsAPI.Application.DTOs;

public class PatientServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsPatientList()
    {
        var mockRepo = new Mock<IPatientRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Patient> { new Patient { Id = 1, Name = "Juan Pérez" } });

        mockMapper.Setup(mapper => mapper.Map<IEnumerable<PatientDTO>>(It.IsAny<IEnumerable<Patient>>()))
            .Returns(new List<PatientDTO> { new PatientDTO { Id = 1, Name = "Juan Pérez" } });

        var service = new PatientService(mockRepo.Object, mockMapper.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("Juan Pérez", result.First().Name);
    }
}
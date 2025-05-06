using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalAppointmentsAPI.Application.Services;
using MedicalAppointmentsAPI.Application.Interfaces;
using MedicalAppointmentsAPI.Domain.Entities;
using MedicalAppointmentsAPI.Application.DTOs;

public class DoctorServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsDoctorList()
    {
        var mockRepo = new Mock<IDoctorRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Doctor> { new Doctor { Id = 1, Name = "Dr. Smith" } });

        mockMapper.Setup(mapper => mapper.Map<IEnumerable<DoctorDTO>>(It.IsAny<IEnumerable<Doctor>>()))
            .Returns(new List<DoctorDTO> { new DoctorDTO { Id = 1, Name = "Dr. Smith" } });

        var service = new DoctorService(mockRepo.Object, mockMapper.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("Dr. Smith", result.First().Name);
    }
}
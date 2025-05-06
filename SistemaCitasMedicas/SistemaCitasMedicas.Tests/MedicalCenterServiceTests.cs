using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalAppointmentsAPI.Application.Services;
using MedicalAppointmentsAPI.Application.Interfaces;
using MedicalAppointmentsAPI.Domain.Entities;
using MedicalAppointmentsAPI.Application.DTOs;

public class MedicalCenterServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsMedicalCenters()
    {
        var mockRepo = new Mock<IMedicalCenterRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<MedicalCenter> {
                new MedicalCenter { Id = 1, Name = "Centro Salud Central" }
            });

        mockMapper.Setup(mapper => mapper.Map<IEnumerable<MedicalCenterDTO>>(It.IsAny<IEnumerable<MedicalCenter>>()))
            .Returns(new List<MedicalCenterDTO> {
                new MedicalCenterDTO { Id = 1, Name = "Centro Salud Central" }
            });

        var service = new MedicalCenterService(mockRepo.Object, mockMapper.Object);

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("Centro Salud Central", result.First().Name);
    }
}
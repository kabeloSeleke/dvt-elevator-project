
using AutoMapper;
using ElevatorEngine.Domain.Interfaces;
using ElevatorSystem.Application.DTOs;
using ElevatorSystem.Application.Mapper;
using ElevatorSystem.Application.Services;
using ElevatorSystem.Domain.Interfaces;
using ElevatorSystem.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace ElevatorSystem.Tests.Services {
    public class FloorServiceTests {
        private readonly FloorService _floorService;
        private readonly Mock<IFloorRepository> _floorRepositoryMock = new Mock<IFloorRepository>();
        private readonly Mock<ILogger<FloorService>> _loggerMock = new Mock<ILogger<FloorService>>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly IMapper _mapper;

        public FloorServiceTests() {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();

            _floorService = new FloorService(_floorRepositoryMock.Object, _mapper, _unitOfWorkMock.Object, _loggerMock.Object);
        }


        [Fact]
        public async Task GetAllFloorStatusesAsync_ReturnsAllFloors() {
            var floors = new List<Floor>
            {
                new Floor { Id = 1 },
                new Floor { Id = 2 }
            };

            _floorRepositoryMock.Setup(repo => repo.GetAllFloorsAsync()).ReturnsAsync(floors);
            var result = await _floorService.GetAllFloorStatusesAsync();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetFloorStatusAsync_ValidId_ReturnsCorrectFloor() {

            var floor = new Floor { Id = 1, FloorNumber = 3 };
            _floorRepositoryMock.Setup(repo => repo.GetFloorByIdAsync(1)).ReturnsAsync(floor);
            var result = await _floorService.GetFloorStatusAsync(1);
            Assert.Equal(1, result.Id);
            Assert.Equal(3, result.FloorNumber);
        }

        [Fact]
        public async Task UpdateFloorAsync_ValidDto_UpdatesFloor() {

            var floor = new Floor { Id = 1 };
            var floorDTO = new FloorDTO { Id = 1, FloorNumber = 5 };
            _floorRepositoryMock.Setup(repo => repo.GetFloorByIdAsync(1)).ReturnsAsync(floor);
            await _floorService.UpdateFloorAsync(floorDTO);
            _floorRepositoryMock.Verify(repo => repo.UpdateFloorAsync(It.IsAny<Floor>()), Times.Once);
        }

        [Fact]
        public async Task CreateFloorAsync_ValidDto_CreatesFloor() {

            var floorDTO = new FloorDTO { Id = 1, FloorNumber = 2 };
            var result = await _floorService.CreateFloorAsync(floorDTO);
            Assert.Equal(2, result.FloorNumber);
            _floorRepositoryMock.Verify(repo => repo.AddFloorAsync(It.IsAny<Floor>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }
    }
}

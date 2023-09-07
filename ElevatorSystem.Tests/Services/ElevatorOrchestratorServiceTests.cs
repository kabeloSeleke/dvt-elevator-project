
using ElevatorSystem.Application.DTOs;
using ElevatorSystem.Application.Interfaces;
using ElevatorSystem.Application.Services;
using ElevatorSystem.Domain.Values;
using Microsoft.Extensions.Logging;
using Moq;

namespace ElevatorSystem.Tests.Services {
 
 public class ElevatorOrchestratorServiceTests {
            private readonly ElevatorOrchestratorService _elevatorOrchestratorService;
            private readonly Mock<IElevatorService> _elevatorServiceMock = new Mock<IElevatorService>();
            private readonly Mock<IFloorService> _floorServiceMock = new Mock<IFloorService>();
            private readonly Mock<ILogger<ElevatorOrchestratorService>> _loggerMock = new Mock<ILogger<ElevatorOrchestratorService>>();

            public ElevatorOrchestratorServiceTests() {
                _elevatorOrchestratorService = new ElevatorOrchestratorService(_elevatorServiceMock.Object, _floorServiceMock.Object, _loggerMock.Object);
            }

            [Fact]
            public async Task RequestElevatorToFloor_ValidRequest_UpdatesFloorAndRequestsElevator() {
    
                int floorId = 1;
                int numOfPeople = 5;
                ElevatorDirection direction = ElevatorDirection.Up;
                var elevatorDTO = new ElevatorDTO { Id = 1 };

                _floorServiceMock.Setup(fs => fs.UpdateFloorOccupantsAsync(floorId, numOfPeople, direction)).Returns(Task.CompletedTask);
                _elevatorServiceMock.Setup(es => es.GetNearestAvailableElevatorAsync(floorId, direction)).ReturnsAsync(elevatorDTO);
                _elevatorServiceMock.Setup(es => es.SendNearestElevatorToFloorAsync(elevatorDTO.Id, direction)).Returns(Task.CompletedTask);

                await _elevatorOrchestratorService.RequestElevatorToFloorAsync(floorId, numOfPeople, direction);

                _floorServiceMock.Verify(fs => fs.UpdateFloorOccupantsAsync(floorId, numOfPeople, direction), Times.Once);
                _elevatorServiceMock.Verify(es => es.GetNearestAvailableElevatorAsync(floorId, direction), Times.Once);
                _elevatorServiceMock.Verify(es => es.SendNearestElevatorToFloorAsync(elevatorDTO.Id, direction), Times.Once);
            }
        }
    }

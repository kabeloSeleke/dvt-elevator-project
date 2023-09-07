using AutoMapper;
using ElevatorEngine.Domain.Interfaces;
using ElevatorSystem.Application.DTOs;
using ElevatorSystem.Application.Mapper;
using ElevatorSystem.Application.Services;
using ElevatorSystem.Domain.Interfaces;
using ElevatorSystem.Domain.Models;
using ElevatorSystem.Domain.Values;
using Microsoft.Extensions.Logging;
using Moq;

namespace ElevatorSystem.Tests.Services {
   

     
        public class ElevatorServiceTests {

        private readonly ElevatorService _elevatorService;
        private readonly Mock<IElevatorRepository> _elevatorRepositoryMock = new Mock<IElevatorRepository>();
        private readonly Mock<ILogger<ElevatorService>> _loggerMock = new Mock<ILogger<ElevatorService>>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly IMapper _mapper;

        public ElevatorServiceTests() {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();
            _elevatorService = new ElevatorService(_elevatorRepositoryMock.Object, _mapper, _unitOfWorkMock.Object, _loggerMock.Object);
        }

        [Fact(Timeout = 5000)]
            public async Task GetAllElevators_ReturnsAllElevatorsAsync() {
                var elevators = new List<Elevator>
                {
                new Elevator { Id = 1 },
                new Elevator { Id = 2 }
            };

                _elevatorRepositoryMock.Setup(repo => repo.GetAllElevatorsAsync()).ReturnsAsync(elevators);
                var result = await _elevatorService.GetAllElevatorsAsync();
                Assert.Equal(2, result.Count());
            }

            [Fact(Timeout = 5000)]
            public async Task GetElevatorStatus_ValidId_ReturnsCorrectElevatorAsync() {
                var elevator = new Elevator { Id = 1, CurrentFloor = 3 };
                _elevatorRepositoryMock.Setup(repo => repo.GetElevatorByIdAsync(1)).ReturnsAsync(elevator);
                var result = await _elevatorService.GetElevatorStatusAsync(1);
                Assert.Equal(1, result.Id);
                Assert.Equal(3, result.CurrentFloor);
            }

            [Fact(Timeout = 5000)]
            public async Task UpdateElevator_ValidDto_UpdatesElevatorAsync() {
                var elevator = new Elevator { Id = 1 };
                var elevatorDTO = new ElevatorDTO { Id = 1, CurrentFloor = 5 };
                _elevatorRepositoryMock.Setup(repo => repo.GetElevatorByIdAsync(1)).ReturnsAsync(elevator);
                await _elevatorService.UpdateElevatorAsync(elevatorDTO);
                _elevatorRepositoryMock.Verify(repo => repo.UpdateElevatorAsync(It.IsAny<Elevator>()), Times.Once);
                _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            }

            [Fact(Timeout = 5000)]
            public async Task CreateElevator_ValidDto_CreatesElevatorAsync() {
                var elevatorDTO = new ElevatorDTO { Id = 1, WeightLimit = 500 };
                var result = await _elevatorService.CreateElevatorAsync(elevatorDTO);
                Assert.Equal(ElevatorStatus.Idle, result.Status);
                Assert.Equal(1, result.CurrentFloor);
                Assert.Equal(0, result.OccupantsCount);
                Assert.Equal(ElevatorDirection.None, result.Direction);
                _elevatorRepositoryMock.Verify(repo => repo.AddElevatorAsync(It.IsAny<Elevator>()), Times.Once);
                _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            }
        }
    }


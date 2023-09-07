using ElevatorSystem.Domain.Models;
using ElevatorSystem.Domain.Values;
using ElevatorSystem.Infrastructure.Data;
using ElevatorSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSystem.Tests.Repositories {
    public class ElevatorRepositoryTests {
        private readonly ApplicationDbContext _context;
        private readonly ElevatorRepository _elevatorRepository;

        public ElevatorRepositoryTests() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _elevatorRepository = new ElevatorRepository(_context);
        }

        [Fact]
        public async Task AddElevator_AddsElevatorSuccessfullyAsync() {
            var elevator = new Elevator {
                Id = 1,
                CurrentFloor = 1,
                Status = ElevatorStatus.Idle,
                Direction = ElevatorDirection.None
            };

            await _elevatorRepository.AddElevatorAsync(elevator);

            var result = await _elevatorRepository.GetElevatorByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetElevatorById_ReturnsCorrectElevatorAsync() {
            var elevator = new Elevator { Id = 2, CurrentFloor = 2, Status = ElevatorStatus.Idle, Direction = ElevatorDirection.None };
            _context.Elevators.Add(elevator);
            await _context.SaveChangesAsync();
            var result = await _elevatorRepository.GetElevatorByIdAsync(2);
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task RemoveElevator_RemovesElevatorSuccessfullyAsync() {
            var elevator = new Elevator { Id = 3, CurrentFloor = 3, Status = ElevatorStatus.Idle, Direction = ElevatorDirection.None };
            _context.Elevators.Add(elevator);
            await _context.SaveChangesAsync();

            await _elevatorRepository.RemoveElevatorAsync(3);
            var result = await _elevatorRepository.GetElevatorByIdAsync(3);

            Assert.Null(result);
        }
    }
}

namespace ElevatorEngine.Tests.Repositories {
    public class FloorRepositoryTests {
        private readonly ApplicationDbContext _context;
        private readonly FloorRepository _floorRepository;

        public FloorRepositoryTests() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _floorRepository = new FloorRepository(_context);
        }

        [Fact]
        public async Task AddFloor_AddsFloorSuccessfullyAsync() {
            var floor = new Floor { Id = 1, FloorNumber = 1, WaitingOccupants = 5, TotalPeopleGoingUp = 3, TotalPeopleGoingDown = 2 };
            await _floorRepository.AddFloorAsync(floor);

            var result = await _floorRepository.GetFloorByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetFloorById_ReturnsCorrectFloorAsync() {
            var floor = new Floor { Id = 2, FloorNumber = 2, WaitingOccupants = 3, TotalPeopleGoingUp = 2, TotalPeopleGoingDown = 1 };
            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();
            var result = await _floorRepository.GetFloorByIdAsync(2);
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task RemoveFloor_RemovesFloorSuccessfullyAsync() {
            var floor = new Floor { Id = 3, FloorNumber = 3, WaitingOccupants = 4, TotalPeopleGoingUp = 2, TotalPeopleGoingDown = 2 };
            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();

            await _floorRepository.RemoveFloorAsync(3);
            var result = await _floorRepository.GetFloorByIdAsync(3);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateFloor_UpdatesFloorSuccessfullyAsync() {
            var floor = new Floor { Id = 4, FloorNumber = 4, WaitingOccupants = 5, TotalPeopleGoingUp = 3, TotalPeopleGoingDown = 2 };
            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();
            floor.WaitingOccupants = 10;
            await _floorRepository.UpdateFloorAsync(floor);
            var updatedFloor = await _floorRepository.GetFloorByIdAsync(4);
            Assert.Equal(10, updatedFloor.WaitingOccupants);
        }

        [Fact]
        public async Task GetAllFloors_ReturnsAllFloorsAsync() {
            var floor1 = new Floor { Id = 5, FloorNumber = 5, WaitingOccupants = 6, TotalPeopleGoingUp = 4, TotalPeopleGoingDown = 2 };
            var floor2 = new Floor { Id = 6, FloorNumber = 6, WaitingOccupants = 7, TotalPeopleGoingUp = 5, TotalPeopleGoingDown = 2 };

            _context.Floors.AddRange(floor1, floor2);
            await _context.SaveChangesAsync();

            var result = await _floorRepository.GetAllFloorsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}

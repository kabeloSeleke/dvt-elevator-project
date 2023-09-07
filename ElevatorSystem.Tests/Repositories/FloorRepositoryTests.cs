

using ElevatorSystem.Domain.Models;
using ElevatorSystem.Infrastructure.Data;
using ElevatorSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSystem.Tests.Repositories {
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
        public async Task AddFloorAsync_AddsFloorSuccessfully() {
            var floor = new Floor { Id = 1, FloorNumber = 1, WaitingOccupants = 5, TotalPeopleGoingUp = 3, TotalPeopleGoingDown = 2 };
            await _floorRepository.AddFloorAsync(floor);

            var result = await _floorRepository.GetFloorByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetFloorByIdAsync_ReturnsCorrectFloor() {
            var floor = new Floor { Id = 2, FloorNumber = 2, WaitingOccupants = 3, TotalPeopleGoingUp = 2, TotalPeopleGoingDown = 1 };
            await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync();

            var result = await _floorRepository.GetFloorByIdAsync(2);
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task RemoveFloorAsync_RemovesFloorSuccessfully() {
            var floor = new Floor { Id = 3, FloorNumber = 3, WaitingOccupants = 4, TotalPeopleGoingUp = 2, TotalPeopleGoingDown = 2 };
            await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync();

            await _floorRepository.RemoveFloorAsync(3);
            var result = await _floorRepository.GetFloorByIdAsync(3);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateFloorAsync_UpdatesFloorSuccessfully() {
            var floor = new Floor { Id = 4, FloorNumber = 4, WaitingOccupants = 5, TotalPeopleGoingUp = 3, TotalPeopleGoingDown = 2 };
            await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync();

            floor.WaitingOccupants = 10;
            await _floorRepository.UpdateFloorAsync(floor);

            var updatedFloor = await _floorRepository.GetFloorByIdAsync(4);
            Assert.Equal(10, updatedFloor.WaitingOccupants);
        }

        [Fact]
        public async Task GetAllFloorsAsync_ReturnsAllFloors() {
            var floor1 = new Floor { Id = 5, FloorNumber = 5, WaitingOccupants = 6, TotalPeopleGoingUp = 4, TotalPeopleGoingDown = 2 };
            var floor2 = new Floor { Id = 6, FloorNumber = 6, WaitingOccupants = 7, TotalPeopleGoingUp = 5, TotalPeopleGoingDown = 2 };

            await _context.Floors.AddRangeAsync(floor1, floor2);
            await _context.SaveChangesAsync();

            var result = await _floorRepository.GetAllFloorsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}

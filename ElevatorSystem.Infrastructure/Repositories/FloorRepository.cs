using ElevatorSystem.Domain.Interfaces;
using ElevatorSystem.Domain.Models;
using ElevatorSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSystem.Infrastructure.Repositories {
    public class FloorRepository : IFloorRepository {
        private readonly ApplicationDbContext _context;

        public FloorRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task AddFloorAsync(Floor floor) {
            _context.Floors.Add(floor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Floor>> GetAllFloorsAsync() {
            return await _context.Floors.ToListAsync();
        }

        public async Task<Floor> GetFloorByIdAsync(int id) {
            return await _context.Floors.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task RemoveFloorAsync(int id) {
            var floor = await _context.Floors.FindAsync(id);
            if (floor != null) {
                _context.Floors.Remove(floor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateFloorAsync(Floor floor) {
            var existingFloor = await _context.Floors.FindAsync(floor.Id);
            if (existingFloor != null) {
                _context.Entry(existingFloor).CurrentValues.SetValues(floor);
                await _context.SaveChangesAsync();
            }
        }
    }
}

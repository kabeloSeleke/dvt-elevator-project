using ElevatorSystem.Domain.Interfaces;
using ElevatorSystem.Domain.Models;
using ElevatorSystem.Domain.Values;
using ElevatorSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSystem.Infrastructure.Repositories {
    public class ElevatorRepository : IElevatorRepository {
        private readonly ApplicationDbContext _context;

        public ElevatorRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task AddElevatorAsync(Elevator elevator) {
            _context.Elevators.Add(elevator);
            await _context.SaveChangesAsync();
        }

        public async Task<Elevator> GetElevatorByIdAsync(int id) {
            return await _context.Elevators.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task RemoveElevatorAsync(int id) {
            var elevator = await _context.Elevators.FirstOrDefaultAsync(e => e.Id == id);
            if (elevator != null) {
                _context.Elevators.Remove(elevator);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateElevatorAsync(Elevator elevator) {
            _context.Elevators.Update(elevator);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Elevator>> GetAllElevatorsAsync() {
            return await _context.Elevators.ToListAsync();
        }

        public async Task<IEnumerable<Elevator>> GetElevatorsByStatusAsync(ElevatorStatus status) {
            return await _context.Elevators.Where(e => e.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Elevator>> GetNearestElevatorAsync(int floorNumber) {
            return await _context.Elevators.OrderBy(e => Math.Abs(e.CurrentFloor - floorNumber)).ToListAsync();
        }
    }
}

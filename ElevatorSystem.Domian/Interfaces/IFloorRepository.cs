using ElevatorSystem.Domain.Models;

namespace ElevatorSystem.Domain.Interfaces {
    public interface IFloorRepository {
        Task<IEnumerable<Floor>> GetAllFloorsAsync();
        Task<Floor> GetFloorByIdAsync(int id);
        Task AddFloorAsync(Floor floor);
        Task UpdateFloorAsync(Floor floor);
        Task RemoveFloorAsync(int id);
    }
}
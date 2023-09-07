using ElevatorSystem.Domain.Models;
using ElevatorSystem.Domain.Values;

namespace ElevatorSystem.Domain.Interfaces {
    public interface IElevatorRepository {
        Task<IEnumerable<Elevator>> GetAllElevatorsAsync();
        Task<Elevator> GetElevatorByIdAsync(int id);
        Task AddElevatorAsync(Elevator elevator);
        Task UpdateElevatorAsync(Elevator elevator);
        Task RemoveElevatorAsync(int id);
        Task<IEnumerable<Elevator>> GetElevatorsByStatusAsync(ElevatorStatus status);
        Task<IEnumerable<Elevator>> GetNearestElevatorAsync(int floorNumber);
    }
}
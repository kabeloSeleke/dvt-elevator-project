using ElevatorSystem.Domain.Values;   
namespace ElevatorSystem.Application.Interfaces {
    public interface IElevatorOrchestratorService {
        Task RequestElevatorToFloorAsync(int floorId, int numOfPeople, ElevatorDirection direction);
    }
}
using ElevatorSystem.Domain.Values;   
namespace ElevatorEngine.Application.Interfaces {
    public interface IElevatorOrchestratorService {
        Task RequestElevatorToFloorAsync(int floorId, int numOfPeople, ElevatorDirection direction);
    }
}
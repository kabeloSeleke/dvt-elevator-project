using ElevatorSystem.Application.DTOs;
using ElevatorSystem.Domain.Values;


namespace ElevatorSystem.Application.Interfaces {
    public interface IFloorService {
        Task<FloorDTO> GetFloorStatusAsync(int floorId);
        Task UpdateFloorAsync(FloorDTO floorDTO);
        Task<FloorDTO> CreateFloorAsync(FloorDTO floorDTO);
        Task RequestElevatorToFloorAsync(int floorId, int numOfPeople, ElevatorDirection direction);
        Task<IEnumerable<FloorDTO>> GetAllFloorStatusesAsync();
        Task UpdateFloorOccupantsAsync(int floorId, int numOfPeople, ElevatorDirection direction);
    }
}




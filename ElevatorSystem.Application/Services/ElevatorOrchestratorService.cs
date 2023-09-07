using ElevatorSystem.Application.Interfaces;
using ElevatorSystem.Domain.Values;
using Microsoft.Extensions.Logging;
namespace ElevatorSystem.Application.Services {
 public class ElevatorOrchestratorService : IElevatorOrchestratorService {
        private readonly IElevatorService _elevatorService;
        private readonly IFloorService _floorService;
        private readonly ILogger<ElevatorOrchestratorService> _logger;
        public ElevatorOrchestratorService(IElevatorService elevatorService, IFloorService floorService , ILogger<ElevatorOrchestratorService> logger) {
            _elevatorService = elevatorService ?? throw new ArgumentNullException(nameof(elevatorService));
            _floorService = floorService ?? throw new ArgumentNullException(nameof(floorService));
        }
        public async Task RequestElevatorToFloorAsync(int floorId, int numOfPeople, ElevatorDirection direction) {
            try {
                await _floorService.UpdateFloorOccupantsAsync(floorId, numOfPeople, direction);

                var nearestElevator = await _elevatorService.GetNearestAvailableElevatorAsync(floorId, direction);
                await _elevatorService.SendNearestElevatorToFloorAsync(nearestElevator.Id, direction);
            }
            catch (Exception ex) {
                _logger.LogError("Error RequestElevatorToFloorAsync");
                throw;
            }
        }
    }
}

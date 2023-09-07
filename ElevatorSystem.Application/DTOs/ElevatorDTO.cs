using ElevatorSystem.Domain.Values;

namespace ElevatorSystem.Application.DTOs {
    public class ElevatorDTO {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public ElevatorStatus Status { get; set; }
        public ElevatorDirection Direction { get; set; }
        public int OccupantsCount { get; set; }
        public int WeightLimit { get; set; }
        public int MaintenanceCount { get; set; }
        public bool IsOnline { get; set; }
        public ElevatorDirection RequestedDirection { get; set; }
        public int TargetFloor { get; set; }   
    }
}
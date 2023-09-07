 

namespace ElevatorSystem.Application.DTOs {
    public class FloorDTO {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public int WaitingOccupants { get; set; }
        public int TotalPeopleGoingUp { get; set; }
        public int TotalPeopleGoingDown { get; set; }
    }

}
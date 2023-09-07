namespace ElevatorSystem.Domain.Models {
    public class Floor {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public int WaitingOccupants { get; set; }
        public int TotalPeopleGoingUp { get; set; }
        public int TotalPeopleGoingDown { get; set; }
    }
}
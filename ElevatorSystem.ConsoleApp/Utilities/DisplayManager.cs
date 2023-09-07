using ElevatorSystem.Application.DTOs;


namespace ElevatorSystem.ConsoleApp.Utilities {
    public static class DisplayManager {
        public static void DisplayElevators(IEnumerable<ElevatorDTO> elevators) {
            Console.WriteLine("Elevator Status:\n");
            foreach (var elevator in elevators) {
                DisplayElevator(elevator);
                Console.WriteLine("------------------------------------");
            }
        }

        private static void DisplayElevator(ElevatorDTO elevator) {
            Console.WriteLine($"ID: {elevator.Id}");
            Console.WriteLine($"Current Floor: {elevator.CurrentFloor}");
            Console.WriteLine($"Direction: {elevator.Direction}");
            Console.WriteLine($"Occupants: {elevator.OccupantsCount}");
        }
    }
}


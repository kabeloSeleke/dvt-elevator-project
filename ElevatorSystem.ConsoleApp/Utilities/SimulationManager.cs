using ElevatorSystem.Application.Interfaces;
using ElevatorSystem.Domain.Values;
using System;

namespace ElevatorSystem.ConsoleApp.Utilities {
    public class SimulationManager {
        private readonly IElevatorOrchestratorService _orchestratorService;
        private readonly IElevatorService _elevatorService;

        public SimulationManager(IElevatorService elevatorService, IElevatorOrchestratorService orchestratorService) {
            _elevatorService = elevatorService ?? throw new ArgumentNullException(nameof(elevatorService));
            _orchestratorService = orchestratorService ?? throw new ArgumentNullException(nameof(orchestratorService));
        }

        public async Task StartAsync() {
            while (true) {
                try {
                    await DisplayElevatorStatusAsync();
                    await GetUserInputAndRequestElevatorAsync();
                }
                catch (FormatException) {
                    Console.WriteLine("Invalid input. Please try again.");
                }
                catch (ArgumentException ex) {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex) {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private async Task DisplayElevatorStatusAsync() {
            var elevators = await _elevatorService.GetAllElevatorsAsync();
            DisplayManager.DisplayElevators(elevators);
        }

        private async Task GetUserInputAndRequestElevatorAsync() {
            Console.WriteLine("Enter floor number to call an elevator:");
            if (!int.TryParse(Console.ReadLine(), out int floorNumber) || floorNumber <= 0) {
                throw new ArgumentException("Invalid floor number. Please enter a positive integer.");
            }

            Console.WriteLine("Enter number of people waiting:");
            if (!int.TryParse(Console.ReadLine(), out int numOfPeople) || numOfPeople < 0) {
                throw new ArgumentException("Invalid number of people. Please enter a non-negative integer.");
            }

            Console.WriteLine("Enter direction (U/D):");
            string userInput = Console.ReadLine().ToUpper();

            if (!string.Equals(userInput, "U", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(userInput, "D", StringComparison.OrdinalIgnoreCase)) {
                throw new ArgumentException("Invalid direction. Please enter 'U' or 'D'.");
            }

            ElevatorDirection direction = userInput == "U" ? ElevatorDirection.Up : ElevatorDirection.Down;

            await _orchestratorService.RequestElevatorToFloorAsync(floorNumber, numOfPeople, direction);
        }
    }
}

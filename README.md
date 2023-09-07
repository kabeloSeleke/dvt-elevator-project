# Elevator System

A simulation of an elevator system designed to move people as efficiently as possible. This system provides a console application interface to interact with elevators, view their status, and request their services.

## Features

- **Elevator Status**: View the status of all elevators, including their current floor, direction of movement, and the number of occupants.
- **Elevator Requests**: Call an elevator to a specific floor and specify the number of people waiting.
- **Multiple Floors Support**: The system can handle multiple floors.
- **Multiple Elevators Support**: The system can manage multiple elevators.
- **Efficient Movement**: The system sends the nearest available elevator to the requesting floor.
- **Weight Limit**: Each elevator has a weight limit, expressed as a number of people.

## Architecture

The system is designed using clean architecture principles and consists of the following main components:

### DTOs

- `ElevatorDTO`: Represents the data transfer object for an elevator.
- `FloorDTO`: Represents the data transfer object for a floor.

### Services

- `IElevatorService`: Interface defining the contract for elevator-related operations.
- `IFloorService`: Interface defining the contract for floor-related operations.
- `IElevatorOrchestratorService`: Interface defining the contract for orchestrating elevator requests.

### Repositories

- `IFloorRepository`: Interface defining the contract for floor-related database operations.
- `IElevatorRepository`: Interface defining the contract for elevator-related database operations.

### Console Application

- `SimulationManager`: Manages the simulation for the console application.
- `DisplayManager`: Responsible for displaying the status of the elevators on the console.

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/kabeloSeleke/dvt-elevator-project.git
   ```

2. Navigate to the project directory:
   ```bash
   cd ElevatorSystem
   ```

3. Run the console application:
   ```bash
   dotnet run
   ```

4. Follow the on-screen prompts to interact with the elevator system.
 

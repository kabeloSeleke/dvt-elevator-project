using AutoMapper;
using ElevatorEngine.Domain.Interfaces;
using ElevatorSystem.Application.DTOs;
using ElevatorSystem.Application.Interfaces;
using ElevatorSystem.Domain.Interfaces;
using ElevatorSystem.Domain.Models;
using ElevatorSystem.Domain.Values;
using Microsoft.Extensions.Logging;

namespace ElevatorSystem.Application.Services {
    public class FloorService : IFloorService {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFloorRepository _floorRepository;
        private readonly ILogger<FloorService> _logger;
        public FloorService(IFloorRepository floorRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<FloorService> logger) {
            _floorRepository = floorRepository ?? throw new ArgumentNullException(nameof(floorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Ensure logger isn't null
        }

        public async Task<FloorDTO> GetFloorStatusAsync(int floorId) {
            try {
                Floor floor = await _floorRepository.GetFloorByIdAsync(floorId);
                if (floor == null) throw new InvalidOperationException($"No floor found with ID: {floorId}");

                return new FloorDTO {
                    Id = floor.Id,
                    FloorNumber = floor.FloorNumber,
                    WaitingOccupants = floor.WaitingOccupants
                };
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error retrieving status for floor with ID: {floorId}.");
                throw;
            }
        }

        public async Task UpdateFloorAsync(FloorDTO floorDTO) {
            if (floorDTO == null) throw new ArgumentNullException(nameof(floorDTO));

            try {
                Floor existingFloor = await _floorRepository.GetFloorByIdAsync(floorDTO.Id);
                if (existingFloor != null) {
                    existingFloor.FloorNumber = floorDTO.FloorNumber;
                    existingFloor.WaitingOccupants = floorDTO.WaitingOccupants;
                    await _floorRepository.UpdateFloorAsync(existingFloor);
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error updating floor with ID: {floorDTO.Id}.");
                throw;
            }
        }

        public async Task<FloorDTO> CreateFloorAsync(FloorDTO floorDTO) {
            if (floorDTO == null) throw new ArgumentNullException(nameof(floorDTO));

            try {
                var floor = _mapper.Map<Floor>(floorDTO);
                await _floorRepository.AddFloorAsync(floor);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<FloorDTO>(floor);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error creating a new floor.");
                throw;
            }
        }

        public async Task RequestElevatorToFloorAsync(int floorId, int numOfPeople, ElevatorDirection direction) {
            try {
                Floor floor = await _floorRepository.GetFloorByIdAsync(floorId);

                if (floor == null) throw new InvalidOperationException($"No floor found with ID: {floorId}");

                if (direction == ElevatorDirection.Up) {
                    floor.TotalPeopleGoingUp += numOfPeople;
                }
                else if (direction == ElevatorDirection.Down) {
                    floor.TotalPeopleGoingDown += numOfPeople;
                }

                floor.WaitingOccupants += numOfPeople;
                _floorRepository.UpdateFloorAsync(floor);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error requesting elevator to floor with ID: {floorId}.");
                throw;
            }
        }

        public async Task<IEnumerable<FloorDTO>> GetAllFloorStatusesAsync() {
            try {
                var floors = await _floorRepository.GetAllFloorsAsync();
                return _mapper.Map<IEnumerable<FloorDTO>>(floors);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error retrieving all floor statuses");
                throw;
            }
        }

        public async Task UpdateFloorOccupantsAsync(int floorId, int numOfPeople, ElevatorDirection direction) {
            try {
                Floor floor = await _floorRepository.GetFloorByIdAsync(floorId);

                if (floor == null) throw new InvalidOperationException($"No floor found with ID: {floorId}");

                
                floor.WaitingOccupants += numOfPeople;

                 
                if (direction == ElevatorDirection.Up) {
                    floor.TotalPeopleGoingUp += numOfPeople;
                }
                else if (direction == ElevatorDirection.Down) {
                    floor.TotalPeopleGoingDown += numOfPeople;
                }

                 
                _floorRepository.UpdateFloorAsync(floor);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error updating occupants for floor with ID: {floorId}.");
                throw;
            }
        }

        
    }
}

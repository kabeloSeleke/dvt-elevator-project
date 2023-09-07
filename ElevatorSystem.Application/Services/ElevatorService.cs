using AutoMapper;
using ElevatorEngine.Domain.Interfaces;
using ElevatorSystem.Application.DTOs;
using ElevatorSystem.Application.Interfaces;
using ElevatorSystem.Domain.Interfaces;
using ElevatorSystem.Domain.Models;
using ElevatorSystem.Domain.Values;
using Microsoft.Extensions.Logging;

namespace ElevatorSystem.Application.Services  {
    public class ElevatorService : IElevatorService {

        private readonly IElevatorRepository _elevatorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ElevatorService> _logger;

        public ElevatorService(IElevatorRepository elevatorRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<ElevatorService> logger) {
            _elevatorRepository = elevatorRepository ?? throw new ArgumentNullException(nameof(elevatorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<IEnumerable<ElevatorDTO>> GetAllElevatorsAsync() {
            try {
                var elevators = await _elevatorRepository.GetAllElevatorsAsync();
                return _mapper.Map<IEnumerable<ElevatorDTO>>(elevators);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error retrieving all elevators.");
                throw;
            }
        }

        public async Task<ElevatorDTO> GetElevatorStatusAsync(int elevatorId) {
            try {
                Elevator elevator = await _elevatorRepository.GetElevatorByIdAsync(elevatorId);
                return _mapper.Map<ElevatorDTO>(elevator);
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error retrieving status for elevator with ID: {elevatorId}.");
                throw;
            }
        }

        public async Task UpdateElevatorAsync(ElevatorDTO elevatorDTO) {
            if (elevatorDTO == null) throw new ArgumentNullException(nameof(elevatorDTO));

            try {
                Elevator existingElevator = await _elevatorRepository.GetElevatorByIdAsync(elevatorDTO.Id);
                if (existingElevator != null) {
                    _mapper.Map(elevatorDTO, existingElevator);
                    await _elevatorRepository.UpdateElevatorAsync(existingElevator);
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error updating elevator with ID: {elevatorDTO.Id}.");
                throw;
            }
        }

        public async Task<ElevatorDTO> CreateElevatorAsync(ElevatorDTO elevatorDTO) {
            if (elevatorDTO == null) throw new ArgumentNullException(nameof(elevatorDTO));

            try {
                var elevator = _mapper.Map<Elevator>(elevatorDTO);
                elevator.Status = ElevatorStatus.Idle;
                elevator.CurrentFloor = 1;
                elevator.OccupantsCount = 0;
                elevator.Direction = ElevatorDirection.None;

                await _elevatorRepository.AddElevatorAsync(elevator);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ElevatorDTO>(elevator);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error creating a new elevator.");
                throw;
            }
        }

        public async Task<IEnumerable<ElevatorDTO>> GetAllElevatorStatusesAsync() {
            return await GetAllElevatorsAsync();
        }

        public async Task<ElevatorDTO> GetNearestAvailableElevatorAsync(int floorId, ElevatorDirection direction) {
            try {
                var nearestElevators = await _elevatorRepository.GetNearestElevatorAsync(floorId);
                var availableElevator = nearestElevators.FirstOrDefault(e => e.Status == ElevatorStatus.Idle || (e.Status == ElevatorStatus.Moving && e.RequestedDirection == direction));

                if (availableElevator == null) {
                    _logger.LogWarning($"No available elevators near floor: {floorId}.");
                    return null;
                }

                return _mapper.Map<ElevatorDTO>(availableElevator);
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error getting nearest available elevator for floor ID: {floorId}.");
                throw;
            }
        }

        public async Task SendNearestElevatorToFloorAsync(int floorNumber, ElevatorDirection direction) {
            try {
                var nearestElevator = (await _elevatorRepository.GetNearestElevatorAsync(floorNumber)).FirstOrDefault();

                if (nearestElevator != null) {
                    await AssignElevatorToFloorAsync(nearestElevator.Id, floorNumber);
                }
                else {
                    _logger.LogWarning($"No elevators found near floor: {floorNumber}.");
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error sending nearest elevator to floor: {floorNumber}.");
                throw;
            }
        }

        public async Task AssignElevatorToFloorAsync(int elevatorId, int floorId) {
            try {
                var elevator = await _elevatorRepository.GetElevatorByIdAsync(elevatorId);
                if (elevator == null) {
                    _logger.LogWarning($"Elevator with ID: {elevatorId} not found.");
                    return;
                }

                if (elevator.CurrentFloor != floorId)  // Check to avoid unnecessary status update
                {
                    elevator.Status = ElevatorStatus.Moving;
                    elevator.RequestedDirection = elevator.CurrentFloor < floorId ? ElevatorDirection.Up : ElevatorDirection.Down;
                    elevator.TargetFloor = floorId;

                    await _elevatorRepository.UpdateElevatorAsync(elevator);
                    _logger.LogInformation($"Elevator with ID: {elevatorId} has been assigned to move to floor: {floorId}.");
                }
                else {
                    _logger.LogInformation($"Elevator with ID: {elevatorId} is already on the floor: {floorId}.");
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Error assigning elevator with ID: {elevatorId} to floor: {floorId}.");
                throw;
            }
        }
    }
}

using ElevatorSystem.Application.DTOs;
using ElevatorSystem.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Application.Interfaces {
    public interface IElevatorService {
        Task<ElevatorDTO> GetElevatorStatusAsync(int elevatorId);
        Task UpdateElevatorAsync(ElevatorDTO elevatorDTO);
        Task<ElevatorDTO> CreateElevatorAsync(ElevatorDTO elevatorDTO);
        Task<IEnumerable<ElevatorDTO>> GetAllElevatorsAsync();
        Task<IEnumerable<ElevatorDTO>> GetAllElevatorStatusesAsync();
        Task<ElevatorDTO> GetNearestAvailableElevatorAsync(int floorId, ElevatorDirection direction);
        Task SendNearestElevatorToFloorAsync(int floorNumber, ElevatorDirection direction);
    }
}
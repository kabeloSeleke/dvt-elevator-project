using AutoMapper;
using ElevatorSystem.Application.DTOs;
using ElevatorSystem.Domain.Models;

namespace ElevatorSystem.Application.Mapper {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<Elevator, ElevatorDTO>().ReverseMap();
            CreateMap<Floor, FloorDTO>().ReverseMap();
        }
    }
}
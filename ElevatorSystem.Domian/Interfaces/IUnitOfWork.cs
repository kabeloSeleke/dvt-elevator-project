
namespace ElevatorEngine.Domain.Interfaces {
    public interface IUnitOfWork {
        Task CommitAsync();
    }
}

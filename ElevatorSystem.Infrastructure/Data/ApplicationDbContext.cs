using ElevatorSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
    namespace ElevatorEngine.Infrastructure.Data {
        public class ApplicationDbContext : DbContext {
            public DbSet<Elevator> Elevators { get; set; }
            public DbSet<Floor> Floors { get; set; }
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options) {
            }
        }
    }
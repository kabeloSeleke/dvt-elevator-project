﻿using ElevatorEngine.Domain.Interfaces;
namespace ElevatorEngine.Infrastructure.Data {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) {
            _context = context;
        }
        public async Task CommitAsync() {
            await _context.SaveChangesAsync();
        }
    }
}
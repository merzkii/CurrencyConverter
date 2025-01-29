using Application.Services;
using Core.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class OperationRepository:IOperationRepository
    {
        private readonly AppDbContext _dbContext;

        public OperationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOperationAsync(Operation operation)
        {
            await _dbContext.Operations.AddAsync(operation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Operation>> GetOperationsAsync()
        {
            return await _dbContext.Operations
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
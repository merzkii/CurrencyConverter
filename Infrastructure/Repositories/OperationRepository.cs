using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class OperationRepository
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

        public async Task<IEnumerable<Operation>> GetAllOperationsAsync()
        {
            return await _dbContext.Operations
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
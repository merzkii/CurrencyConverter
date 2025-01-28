using Core.Models;

namespace Infrastructure.Repositories
{
    public interface IOperationRepository
    {
        Task AddOperationAsync(Operation operation);
        Task<IEnumerable<Operation>> GetOperationsAsync();
    }
}

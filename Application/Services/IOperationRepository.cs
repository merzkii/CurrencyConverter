using Core.Models;

namespace Application.Services
{
    public interface IOperationRepository
    {
        Task AddOperationAsync(Operation operation);
        Task<IEnumerable<Operation>> GetOperationsAsync();
    }
}

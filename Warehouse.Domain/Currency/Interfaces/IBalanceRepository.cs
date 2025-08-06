using Warehouse.Domain.Currency.Entities;

namespace Warehouse.Domain.Currency.Interfaces;

public interface IBalanceRepository
{
    Task<Balance?> GetByIdAsync(Guid id);
    Task<IEnumerable<Balance>> GetAllAsync();
    Task AddAsync(Balance balance);
    Task UpdateAsync(Balance balance);
    Task DeleteAsync(Guid id);
}
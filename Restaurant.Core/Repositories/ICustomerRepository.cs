using Restaurant.Core.DTOS;

namespace Restaurant.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(CustomerDto customer);
    }
}

using Restaurant.Core.DTOS;

namespace Restaurant.Core.Services
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerDto customer);
    }
}

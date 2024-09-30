using Restaurant.Core.DTOS;
using Restaurant.Core.Repositories;
using Restaurant.Core.Services;
using Restaurant.Repositories;

namespace Restaurant.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task AddCustomerAsync(CustomerDto customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
        }

    }

}

using Restaurant.Core.DTOS;
using Restaurant.Core.Repositories;

namespace Restaurant.Repositories
{
    public class CustomerRepository : ICustomerRepository
    { 
        public async Task AddCustomerAsync(CustomerDto customer)
        {
            var _context = new RestaurantDbContext();
            var newCustomer = new Customer
                {
                    Name = customer.Name,
                    Celno = customer.Celno // Assuming you have this field
                };

                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();
            }
        
    }
}

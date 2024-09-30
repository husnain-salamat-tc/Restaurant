using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.DTOS;
using Restaurant.Core.Repositories;
using System.Data;
namespace Restaurant.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly string _connectionString = "Server=DESKTOP-EQ55Q8H;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0;";

        public async Task AddOrderAsync(OrderDto order)
        {
            using (var _context = new RestaurantDbContext())
            {

                // Create a new Order entity
                Order order1 = new Order
                {
                    //Id = order.Id,
                    Bill = order.Bill,
                };

                // Add the Order entity to the context
                _context.Orders.Add(order1);

                // Save changes to the database
               await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Name of the stored procedure
                var query = "DeleteOrder";

                using (var command = new SqlCommand(query, connection))
                {
                    // Set the command type to Stored Procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    command.Parameters.AddWithValue("@Id", orderId);

                    // Execute the command
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Order Deleted Successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Order not found.");
                    }
                }
            }
        }



        public async Task UpdateOrderAsync(OrderDto updatedOrder)
        {
            // Initialize the DbContext
            using (var _context = new RestaurantDbContext())
            {
                // Find the order by its Id
                var orderToUpdate = await _context.Orders.FindAsync(updatedOrder.Id);

                if (orderToUpdate != null)
                {
                    // Update the order's properties with the new values
                    orderToUpdate.Bill = updatedOrder.Bill;

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Order Updated Successfully..");
                }
                else
                {
                    // Optionally handle the case where the order was not found
                    Console.WriteLine("Order not found.");
                }
            }
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            using (var _context = new RestaurantDbContext())
            {
                // Retrieve all orders from the Orders table
                var orders = await _context.Orders.ToListAsync();

                // Map the Order entities to OrderDto objects
                var orderDtos = orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    Bill = o.Bill,
                }).ToList();

                // Return the list of OrderDto objects
                return orderDtos;
            }
        }
        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            // Initialize the DbContext
            using (var _context = new RestaurantDbContext())
            {
                // Find the order by its Id
                var order = await _context.Orders.FindAsync(orderId);

                if (order == null)
                {
                    return null; // or handle as needed, e.g., throw an exception or return a default object
                }
                // Return the order object (null if not found)
                var order1 = new OrderDto();
                order1.Id = orderId;
                order1.Bill = order.Bill;
                return order1;
            }
        }


    }
}
using Restaurant.Core.DTOS;
using Restaurant.Core.Repositories;
using Restaurant.Core.Services;
using Restaurant.Repositories;

namespace Restaurant.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AddOrderAsync(OrderDto order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
        }

        public async Task UpdateOrderAsync(OrderDto updatedOrder)
        {
            await _orderRepository.UpdateOrderAsync(updatedOrder);
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orderDto = await _orderRepository.GetAllOrdersAsync();
            return orderDto;
        }

        public async Task DisplayAllOrdersAsync()
        {
            // Call the GetAllOrdersAsync method to retrieve the orders
            var orderDtos = await GetAllOrdersAsync();

            Console.WriteLine("HERE ARE THE ORDERS THAT ARE CURRENTLY ACTIVE\n\n");
            // Loop through the list of OrderDto and print the details
            foreach (var order in orderDtos)
            {
                Console.WriteLine($"Order ID: {order.Id}");
                Console.WriteLine($"Bill: {order.Bill}");
                Console.WriteLine("------------------------------------");
            }
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order; // Returns null if not found, or you can handle it differently
        }
    }


}


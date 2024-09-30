using Restaurant.Core.DTOS;

namespace Restaurant.Core.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(OrderDto order);
        Task DeleteOrderAsync(int orderId);
        Task UpdateOrderAsync(OrderDto updatedOrder);
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int orderId);
    }
}

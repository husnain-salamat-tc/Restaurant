using Restaurant.Core.DTOS;

namespace Restaurant.Core.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderDto order);
        Task DeleteOrderAsync(int order);
        Task UpdateOrderAsync(OrderDto updatedOrder);
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task DisplayAllOrdersAsync();
        public Task<OrderDto> GetOrderByIdAsync(int orderId);
    }
}
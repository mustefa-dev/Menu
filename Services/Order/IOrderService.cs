using Menu.Dtos;

namespace Menu.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrders();
        Task<OrderDto> GetOrderById(int id);
        Task<string> CreateOrder(OrderDto orderDto);
        Task UpdateOrder(OrderDto orderDto);
        Task DeleteOrder(int id);
        Task<bool> OrderExists(int id);

    }
}
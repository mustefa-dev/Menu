// OrderService.cs

using Auth.Data;
using AutoMapper;
using Menu.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public OrderService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<string> CreateOrder(OrderDto orderDto)
        {
            if (await OrderExists(orderDto.Id))
            {
                return "Order with the same ID already exists.";
            }

            var order = _mapper.Map<Models.Order>(orderDto);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return "Order created successfully.";
        }

        public async Task UpdateOrder(OrderDto orderDto)
        {
            var order = await _context.Orders.FindAsync(orderDto.Id);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            _mapper.Map(orderDto, order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> OrderExists(int id)
        {
            return await _context.Orders.AnyAsync(order => order.Id == id);
        }
    }
}

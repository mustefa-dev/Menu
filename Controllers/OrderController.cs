using AutoMapper;
using Menu.Data.Repositories;
using Menu.Dtos.Order;
using Menu.Services;
using Microsoft.AspNetCore.Mvc;
namespace Menu.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderReadDto>>> GetOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return Ok(_mapper.Map<List<OrderReadDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderReadDto>(order));
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> CreateOrder(OrderCreateDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, _mapper.Map<OrderReadDto>(createdOrder));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderUpdateDto orderDto)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _mapper.Map(orderDto, order);
            await _orderRepository.UpdateOrderAsync(id, order);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteOrderAsync(id);

            return NoContent();
        }
    }
}

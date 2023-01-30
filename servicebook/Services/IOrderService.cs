using transport.Models;

namespace transport.Services
{
    public interface IOrderService
    {
        int CreateOrder(CreateOrderDto dto);
        bool DeleteOrder(int id);
        OrderDto Get(int id);
        IEnumerable<OrderDto> GetAll();
    }
}
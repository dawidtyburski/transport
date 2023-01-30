using transport.Models;

namespace transport.Services
{
    public interface IOrderService
    {
        int CreateOrder(CreateOrderDto dto);
        bool Delete(int id);
        bool Edit(int id, EditOrderDto dto);
        OrderDto Get(int id);
        IEnumerable<OrderDto> GetAll();
    }
}